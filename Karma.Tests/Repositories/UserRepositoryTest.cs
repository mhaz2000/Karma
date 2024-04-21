using FakeItEasy;
using FluentAssertions;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories;

namespace Karma.Tests.Repositories
{
    public class UserRepositoryTest
    {
        private readonly UserRepository _userRepository;
        private readonly DataContext _dataContext;

        public UserRepositoryTest()
        {
            _dataContext = A.Fake<DataContext>();
            _userRepository = new UserRepository(_dataContext);
        }

        [Fact]
        public async Task Must_Create_User()
        {
            //Arrange
            var phoneNumber = "091098289263";

            //Assert
            await _userRepository.Invoking(c=> c.CreateUserAsync(phoneNumber))
                .Should().NotThrowAsync();
        }
    }
}
