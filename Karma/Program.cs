using Karma.API.Extensions;
using Karma.API.Middlewares;
using Karma.Application;
using Karma.Application.Base;
using Karma.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMemoryCache();

builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("Default") ?? throw new Exception("Default Connection string cannot be found."),
    builder.Configuration.GetConnectionString("Log") ?? throw new Exception("Log Connection string cannot be found."));

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

app.MigrateDatabase();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
