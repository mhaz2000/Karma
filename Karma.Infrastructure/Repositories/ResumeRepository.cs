﻿using Karma.Core.Entities;
using Karma.Core.Repositories;
using Karma.Infrastructure.Data;
using Karma.Infrastructure.Repositories.Base;

namespace Karma.Infrastructure.Repositories
{
    public class ResumeRepository : Repository<Resume>, IResumeRepository
    {
        public ResumeRepository(DataContext dataContext) : base(dataContext) 
        {
            
        }
    }
}
