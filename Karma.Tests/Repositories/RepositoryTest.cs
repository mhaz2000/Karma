﻿using Bogus;
using Karma.Core.Entities;
using Karma.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Karma.Tests.Repositories
{
    public class RepositoryTest : IDisposable
    {
        protected DataContext _dataContext { get; private set; }

        protected RepositoryTest()
        {
            InitializeDataContext();
            FakeRoleIdentity().Wait();
        }

        private static Random random = new Random();

        private void InitializeDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase" + Guid.NewGuid().ToString())
                .Options;

            _dataContext = new DataContext(options);
        }

        private async Task FakeRoleIdentity()
        {
            var generatedRoles = GenerateIdentityRoleData();
            await _dataContext.Roles.AddRangeAsync(generatedRoles);
            await _dataContext.SaveChangesAsync();

            var generatedUsers = GenerateUserData(10);

            var userStore = new UserStore<User, IdentityRole<Guid>, DataContext, Guid>(_dataContext);
            var userManager = new UserManager<User>(userStore, null, new PasswordHasher<User>(), null, null, null, null, null, null);

            foreach (var newUser in generatedUsers)
            {
                await userManager.CreateAsync(newUser, "123456");
                await userManager.AddToRoleAsync(newUser, generatedRoles[random.Next(0, 2)].NormalizedName);
            }
        }

        private List<IdentityRole<Guid>> GenerateIdentityRoleData()
        {
            return new List<IdentityRole<Guid>>()
            {
                new IdentityRole<Guid>()
                {
                    Name = "Admin",
                    Id = Guid.NewGuid(),
                    NormalizedName = "admin"
                },
                new IdentityRole<Guid>()
                {
                    Name = "User",
                    Id = Guid.NewGuid(),
                    NormalizedName = "user"
                }
            };
        }


        private List<User> GenerateUserData(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(c => c.UserName, f => f.Name.FindName().Replace(" ", ""))
                .RuleFor(c => c.FirstName, f => f.Person.FirstName)
                .RuleFor(c => c.LastName, f => f.Person.LastName)
                .RuleFor(c => c.Id, f => Guid.NewGuid());

            return faker.Generate(count);
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
