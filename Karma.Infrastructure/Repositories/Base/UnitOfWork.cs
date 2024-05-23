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

        private CityRepository _cityRepository;
        private CountryRepository _countryRepository;
        private UserRepository _userRepository;
        private MajorRepository _majorRepository;
        private RoleRepository _roleRepository;
        private ResumeRepository _resumeRepository;
        private UniversityRepository _universityRepository;
        private JobCategoryRepository _jobCategoryRepository;
        private SocialMediaRepository _socialMediaRepository;
        private EducationalRecordRepository _educationalRecordRepository;
        private CareerRecordRepository _careerRecordRepository;

        public UnitOfWork(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context, _userManager);
        public ICountryRepository CountryRepository => _countryRepository ?? new CountryRepository(_context);
        public ICityRepository CityRepository => _cityRepository ?? new CityRepository(_context);
        public IRoleRepository RoleRepository => _roleRepository ?? new RoleRepository(_context);
        public IMajorRepository MajorRepository => _majorRepository ?? new MajorRepository(_context);
        public IResumeRepository ResumeRepository => _resumeRepository ?? new ResumeRepository(_context);
        public IUniversityRepository UniversityRepository => _universityRepository ?? new UniversityRepository(_context);
        public IJobCategoryRepository JobCategoryRepository => _jobCategoryRepository ?? new JobCategoryRepository(_context);
        public ISocialMediaRepository SocialMediaRepository => _socialMediaRepository ?? new SocialMediaRepository(_context);
        public ICareerRecordRepository CareerRecordRepository => _careerRecordRepository ?? new CareerRecordRepository(_context);
        public IEducationalRecordRepository EducationalRecordRepository => _educationalRecordRepository ?? new EducationalRecordRepository(_context);

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
