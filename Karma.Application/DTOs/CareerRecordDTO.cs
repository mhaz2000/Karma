using Karma.Application.Helpers;
using Karma.Application.Mappings;
using Karma.Core.Entities;
using Karma.Core.Enums;
using System.Globalization;

namespace Karma.Application.DTOs
{
    public class CareerRecordDTO : IMapFrom<CareerRecord>
    {
        public Guid Id { get; set; }
        public required string JobTitle { get; set; }
        public virtual required JobCategoryDTO JobCategory { get; set; }
        public SeniorityLevel SeniorityLevel { get; set; }
        public required string CompanyName { get; set; }
        public virtual required CountryDTO Country { get; set; }
        public virtual required CityDTO? City { get; set; }
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public int? ToMonth { get; set; }
        public int? ToYear { get; set; }
        public bool CurrentJob { get; set; }
        public int WorkTotalMonths => CalculateTotalMonths();
        public string Duration => CalculateDuration();

        public string CalculateDuration()
        {
            string duration = $"از {PersianMonthHelper.GetMonth(FromMonth)} {FromYear} تا ";
            if (ToMonth is null)
                duration += "هم اکنون";
            else
                duration += $"{PersianMonthHelper.GetMonth(ToMonth.Value)} {ToYear}";

            return duration;

        }
        public int CalculateTotalMonths()
        {
            int endMonth = ToMonth ?? DateTime.Now.Month;
            int endYear = ToYear ?? DateTime.Now.Year;

            PersianCalendar pc = new PersianCalendar();

            DateTime fromDate = new DateTime(FromYear, FromMonth, 1, pc);
            DateTime toDate = new DateTime(endYear, endMonth, 1, pc);

            int totalMonths = ((toDate.Year - fromDate.Year) * 12) + toDate.Month - fromDate.Month;

            return totalMonths;
        }
    }
}
