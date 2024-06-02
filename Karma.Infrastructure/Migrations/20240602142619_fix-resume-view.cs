using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixresumeview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
				CREATE Or alter VIEW resumes_info as
				select distinct
					r.Id,
					r.Code,
					r.MainJobTitle,
					r.Description,
					u.Id as UserId,
					u.BirthDate,
					u.City,
					u.FirstName,
					u.LastName,
					u.Gender,
					u.MaritalStatus,
					u.MilitaryServiceStatus,
					u.PhoneNumber,
					u.Telephone,
					er.DegreeLevel,
					er.DiplomaMajor,
					er.GPA,
					er.FromYear as UniversityFromYear,
					er.ToYear UniversityToYear,
					er.StillEducating,
					m.Title as MajorTitle,
					m.Id as MajorId
					uni.Title as UniversityTitle,
					uni.Id as UniversityId,
					cr.JobTitle,
					cr.SeniorityLevel,
					cr.CompanyName,
					cr.FromMonth as CareerRecordFromMonth,
					cr.FromYear as CareerRecordFromYear,
					cr.ToMonth as CareerRecordToMonth,
					cr.ToYear as CareerRecordToYear,
					cr.CurrentJob,
					jc.Title as JobCategory,
					l.LanguageLevel,
					sl.Title as [Language],
					sl.Id as LanguageId,
					ss.SoftwareSkillLevel,
					sss.Id as SoftwareSkillId,
					sss.Title as SoftwareSkill,
					ads.Title as AdditionalSkill
				from Resumes r
				join AspNetUsers u on u.Id = r.UserId
				left join EducationalRecords er on er.ResumeId = r.Id
				left join Majors m on m.id = er.MajorId
				left join Universities uni on uni.Id = er.UniversityId
				left join CareerRecords cr on cr.ResumeId = r.id
				left join JobCategories jc on jc.Id = cr.JobCategoryId
				left join Countries co on co.Id = cr.CountryId
				left join Cities ci on ci.Id = cr.CityId
				left join Languages l on l.ResumeId = r.Id
				left join SystemLanguages sl on sl.Id = l.SystemLanguageId
				left join SoftwareSkills ss on ss.ResumeId = r.Id
				left join SystemSoftwareSkills sss on sss.Id = ss.SystemSoftwareSkillId
				left join AdditionalSkills ads on ads.ResumeId = r.Id
				where u.PhoneNumberConfirmed = 1
");

        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
