﻿using Karma.Core.Entities;
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
        private readonly DataContext _dataContext;
        private readonly LogContext _logContext;

        public DatabaseMigration(IConfiguration configuration)
        {
            _dataContext = new DateContextFactory().CreateDbContext(configuration);
            _logContext = new LogContextFactory().CreateDbContext(configuration);

        }
        public async Task MigrateDatabase()
        {
            _dataContext.Database.Migrate();
            _logContext.Database.Migrate();

            var userStore = new UserStore<User, IdentityRole<Guid>, DataContext, Guid>(_dataContext);
            var userManager = new UserManager<User>(userStore, null, new PasswordHasher<User>(), null, null, null, null, null, null);

            await CreateRolesSeed(_dataContext);
            await CreateAdminSeed(_dataContext, userManager);

            await SeedMajors(_dataContext);
            await SeedUniversities(_dataContext);
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
    }
}
