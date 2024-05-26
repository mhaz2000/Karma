using FluentValidation;
using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;
using Karma.Core.Enums;

namespace Karma.Application.Commands
{
    public class AddCareerRecordCommand : IBaseCommand
    {
        public required string JobTitle { get; set; }
        public int JobCategoryId { get; set; }
        public SeniorityLevel SeniorityLevel { get; set; }
        public required string CompanyName { get; set; }
        public int CountryId { get; set; }
        public int? CityId { get; set; }
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public int? ToMonth { get; set; }
        public int? ToYear { get; set; }
        public bool CurrentJob { get; set; }

        public void Validate() => new AddCareerRecordCommandValidator().Validate(this).RaiseExceptionIfRequired();

    }
}
