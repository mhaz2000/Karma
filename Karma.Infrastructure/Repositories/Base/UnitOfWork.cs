using Karma.Core.Repositories;
using Karma.Core.Repositories.Base;
using Karma.Infrastructure.Data;

namespace Karma.Infrastructure.Repositories.Base
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        private UserRepository _userRepository;
        private RoleRepository _roleRepository;

        public UnitOfWork(DataContext context) => _context = context;

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
        public IRoleRepository RoleRepository => _roleRepository ?? new RoleRepository(_context);

        public async Task<int> CommitAsync()
        {
            var result = await _context.SaveChangesAsync();
            Dispose();
            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
