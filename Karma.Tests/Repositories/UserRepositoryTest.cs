using FakeItEasy;
using FluentAssertions;
using Karma.Core.Entities;
using Karma.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Karma.Tests.Repositories
{
    public class UserRepositoryTest : RepositoryTest
    {
        private readonly UserRepository _userRepository;

        public UserRepositoryTest()
        {
            _userRepository = new UserRepository(_dataContext, A.Fake<UserManager<User>>());
        }

        [Fact]
        public async Task Must_Create_User()
        {
            //Arrange
            var phoneNumber = "091098289263";

            //Act
            await _userRepository.Invoking(c=> c.CreateUserAsync(phoneNumber))
                .Should().NotThrowAsync();

            _dataContext.SaveChanges();

            //Assert
            _dataContext.Users.Where(c=> c.PhoneNumber == phoneNumber).Should().HaveCount(1);
            _dataContext.Users.FirstOrDefault(c => c.PhoneNumber == phoneNumber)?.PhoneNumber.Should().Be(phoneNumber);
        }
    }
}
