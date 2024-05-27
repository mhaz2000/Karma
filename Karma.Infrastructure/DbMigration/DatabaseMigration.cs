using Karma.Core.Entities;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Factories.ContextsFactories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Karma.Infrastructure.DbMigration
{
    public class DatabaseMigration : IDatabaseMigration
    {
        private DataContext _dataContext;
        private LogContext _logContext;

        public DatabaseMigration()
        {
        }
        public async Task MigrateDatabase(string dbConnectionString, string logConnectionString)
        {
            _dataContext = new DateContextFactory().CreateDbContext(dbConnectionString);
            _logContext = new LogContextFactory().CreateDbContext(logConnectionString);

            try
            {

                _dataContext.Database.Migrate();
                _logContext.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("exception");
                throw;
            }

            var userStore = new UserStore<User, IdentityRole<Guid>, DataContext, Guid>(_dataContext);
            var userManager = new UserManager<User>(userStore, null, new PasswordHasher<User>(), null, null, null, null, null, null);

            await CreateRolesSeed(_dataContext);
            await CreateAdminSeed(_dataContext, userManager);

            await SeedMajors(_dataContext);
            await SeedUniversities(_dataContext);
            await SeedCities(_dataContext);
            await SeedCountries(_dataContext);
            await SeedJobCategories(_dataContext);
            await SeedLanguages(_dataContext);
            await SeedSoftwareSkills(_dataContext);
        }

        private static async Task CreateAdminSeed(DataContext context, UserManager<User> userManager)
        {
            string username = "admin";

            if (!context.Users.Any(u => u.UserName == username))
            {
                var newUser = new User("admin", "کاربر", "ادمین")
                {
                    NormalizedUserName = "admin",
                    PhoneNumberConfirmed = true
                };

                var done = await userManager.CreateAsync(newUser, "123456");

                if (done.Succeeded)
                    await userManager.AddToRoleAsync(newUser, "Admin");

            }

            await context.SaveChangesAsync();
        }

        private static async Task CreateRolesSeed(DataContext context)
        {
            var role = await context.Roles.AnyAsync(c => c.Name == "Admin");

            if (!role)
            {
                var newAdminRole = new IdentityRole<Guid>()
                {
                    Name = "Admin",
                    Id = Guid.NewGuid(),
                    NormalizedName = "admin"
                };

                await context.Roles.AddAsync(newAdminRole);

                var newUserRole = new IdentityRole<Guid>()
                {
                    Name = "User",
                    NormalizedName = "user",
                    Id = Guid.NewGuid(),
                };
                await context.Roles.AddAsync(newUserRole);

            }

            context.SaveChanges();
        }

        private static async Task SeedMajors(DataContext context)
        {
            if (!context.Majors.Any())
            {
                var filePath = Directory.GetCurrentDirectory() + "/StaticFiles/majors.json";
                using FileStream stream = File.OpenRead(filePath);
                var majors = await JsonSerializer.DeserializeAsync<ICollection<string>>(stream);

                await context.Majors.AddRangeAsync(majors!.Select(s=> new Major() { Title = s}));
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedUniversities(DataContext context)
        {
            if (!context.Universities.Any())
            {
                var filePath = Directory.GetCurrentDirectory() + "/StaticFiles/universities.json";
                using FileStream stream = File.OpenRead(filePath);
                var universities = await JsonSerializer.DeserializeAsync<ICollection<string>>(stream);

                await context.Universities.AddRangeAsync(universities!.Select(s => new University() { Title = s }));
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedCountries(DataContext context)
        {
            if (!context.Countries.Any())
            {
                var filePath = Directory.GetCurrentDirectory() + "/StaticFiles/countries.json";
                using FileStream stream = File.OpenRead(filePath);
                var countries = await JsonSerializer.DeserializeAsync<ICollection<string>>(stream);

                await context.Countries.AddRangeAsync(countries!.Select(s => new Country() { Title = s }));
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedCities(DataContext context)
        {
            if (!context.Cities.Any())
            {
                var filePath = Directory.GetCurrentDirectory() + "/StaticFiles/cities.json";
                using FileStream stream = File.OpenRead(filePath);
                var cities = await JsonSerializer.DeserializeAsync<ICollection<string>>(stream);

                await context.Cities.AddRangeAsync(cities!.Select(s => new City() { Title = s }));
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedJobCategories(DataContext context)
        {
            if (!context.JobCategories.Any())
            {
                var filePath = Directory.GetCurrentDirectory() + "/StaticFiles/job-categories.json";
                using FileStream stream = File.OpenRead(filePath);
                var jobCategories = await JsonSerializer.DeserializeAsync<ICollection<string>>(stream);

                await context.JobCategories.AddRangeAsync(jobCategories!.Select(s => new JobCategory() { Title = s }));
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedLanguages(DataContext context)
        {
            if (!context.SystemLanguages.Any())
            {
                var filePath = Directory.GetCurrentDirectory() + "/StaticFiles/languages.json";
                using FileStream stream = File.OpenRead(filePath);
                var languages = await JsonSerializer.DeserializeAsync<ICollection<string>>(stream);

                await context.SystemLanguages.AddRangeAsync(languages!.Select(s => new SystemLanguage() { Title = s }));
                await context.SaveChangesAsync();
            }
        }
        
        private static async Task SeedSoftwareSkills(DataContext context)
        {
            if (!context.SystemSoftwareSkills.Any())
            {
                var filePath = Directory.GetCurrentDirectory() + "/StaticFiles/software-skills.json";
                using FileStream stream = File.OpenRead(filePath);
                var softwareSkills = await JsonSerializer.DeserializeAsync<ICollection<string>>(stream);

                await context.SystemSoftwareSkills.AddRangeAsync(softwareSkills!.Select(s => new SystemSoftwareSkill() { Title = s }));
                await context.SaveChangesAsync();
            }
        }
    }
}
