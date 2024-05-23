using Karma.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Karma.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Resume> Resumes { get; set; }
        public virtual DbSet<AdditionalSkill> AdditionalSkills { get; set; }
        public virtual DbSet<SoftwareSkill> SoftwareSkills { get; set; }
        public virtual DbSet<CareerRecord> CareerRecords { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<EducationalRecord> EducationalRecords { get; set; }
        public virtual DbSet<JobCategory> JobCategories { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<SocialMedia> SocialMedias { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
