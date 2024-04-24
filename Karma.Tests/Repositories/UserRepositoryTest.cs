using FakeItEasy;
using FluentAssertions;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories;

namespace Karma.Tests.Repositories
{
    public class UserRepositoryTest : RepositoryTest
    {
        private readonly UserRepository _userRepository;

        public UserRepositoryTest()
        {
            _userRepository = new UserRepository(_dataContext);
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
