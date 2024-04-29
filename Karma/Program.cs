using Karma.API.Extensions;
using Karma.API.Middlewares;
using Karma.Application;
using Karma.Application.Base;
using Karma.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(80); // Use port 80 for HTTP
    options.ListenAnyIP(4030);
});

// Add services to the container.

builder.Services.AddMemoryCache();

var sqlPassword = Environment.GetEnvironmentVariable("SQL_Password");
var sqlUsername = Environment.GetEnvironmentVariable("SQL_Username");
var sqlHost = Environment.GetEnvironmentVariable("SQL_Host");

string dbConnectionString;
string logConnectionString;

if (!string.IsNullOrEmpty(sqlPassword) && !string.IsNullOrEmpty(sqlUsername) && !string.IsNullOrEmpty(sqlHost))
{
    dbConnectionString = $"Data Source ={sqlHost};Initial Catalog = KarmaDB;Persist Security Info=True;Integrated Security=False;User ID ={sqlUsername};" +
        $" Password={sqlPassword};Connect Timeout=15000;MultipleActiveResultSets=true;TrustServerCertificate=True;";

    logConnectionString = $"Data Source ={sqlHost}; Initial Catalog = KarmaLogDB;Persist Security Info=True;Integrated Security=False;User ID ={sqlUsername};" +
        $" Password={sqlPassword};Connect Timeout=15000;;MultipleActiveResultSets=true;TrustServerCertificate=True;";
}
else
{
    dbConnectionString = builder.Configuration.GetConnectionString("Default") ?? throw new Exception("Default Connection string cannot be found.");
    logConnectionString = builder.Configuration.GetConnectionString("Log") ?? throw new Exception("Log Connection string cannot be found.");
}

builder.Services.AddInfrastructure(dbConnectionString, logConnectionString);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddApplicationAuthentication(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new PersianDateTimeConverter());
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.MigrateDatabase(dbConnectionString, logConnectionString);

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
