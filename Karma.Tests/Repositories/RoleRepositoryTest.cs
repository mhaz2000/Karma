using FluentAssertions;
using Karma.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Karma.Tests.Repositories
{
    public class RoleRepositoryTest : RepositoryTest
    {
        private readonly RoleRepository _roleRepository;

        public RoleRepositoryTest()
        {
            _roleRepository = new RoleRepository(_dataContext);
        }

        [Fact]
        public async Task Must_Get_User_Roles()
        {
            //Act
            var user = await _dataContext.Users.LastOrDefaultAsync();
            var roles = await _roleRepository.GetUserRolesAsync(user!);

            //Assert
            roles.Should().NotBeNull();
            roles.Should().HaveCountGreaterThan(0);
        }
    }
}
