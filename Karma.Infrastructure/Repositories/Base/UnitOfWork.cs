﻿using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Core.Repositories.Base;
using Karma.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Karma.Infrastructure.Repositories.Base
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        private UserRepository _userRepository;
        private RoleRepository _roleRepository;
        private ResumeRepository _resumeRepository;

        public UnitOfWork(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context, _userManager);
        public IRoleRepository RoleRepository => _roleRepository ?? new RoleRepository(_context);
        public IResumeRepository ResumeRepository => _resumeRepository ?? new ResumeRepository(_context);

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
