using FluentValidation;
using Karma.Application.Commands;
using Karma.Core.Enums;
using System.Globalization;

namespace Karma.Application.Validators
{
    internal class AddEducationalRecordCommandValidator : AbstractValidator<AddEducationalRecordCommand>
    {
        public AddEducationalRecordCommandValidator()
        {
            var pc = new PersianCalendar();
            RuleFor(c => c.GPA).GreaterThanOrEqualTo(0).When(c => c.GPA is not null)
                .WithMessage("مقدار وارد شده برای معدل صحیح نیست.");
            
            RuleFor(c => c.GPA).LessThanOrEqualTo(20).When(c => c.GPA is not null)
                .WithMessage("مقدار وارد شده برای معدل صحیح نیست.");

            RuleFor(c => c.FromYear).GreaterThan(0).When(c => c.FromYear is not null)
                .WithMessage("مقدار وارد شده برای سال شروع صحیح نیست.");

            RuleFor(c => c.FromYear).LessThanOrEqualTo(pc.GetYear(DateTime.Now)).When(c => c.FromYear is not null)
                .WithMessage("مقدار وارد شده برای سال شروع صحیح نیست.");

            RuleFor(c => c.ToYear).LessThanOrEqualTo(pc.GetYear(DateTime.Now)).When(c => c.ToYear is not null)
                .WithMessage("مقدار وارد شده برای سال پایان صحیح نیست.");

            RuleFor(c => c.ToYear).GreaterThanOrEqualTo(c => c.FromYear).When(c => c.ToYear is not null)
                .WithMessage("سال شروع نمی‌تواند از سال پایان بزرگتر باشد.");

            RuleFor(c => c.DiplomaMajor).Must(c => !string.IsNullOrEmpty(c)).When(c => c.DegreeLevel == DegreeLevel.Diploma)
                .WithMessage("نام رشته تحصیلی الزامی است.");

            RuleFor(c => c.DiplomaMajor).Must(c => string.IsNullOrEmpty(c)).When(c => c.DegreeLevel != DegreeLevel.Diploma)
                .WithMessage("اطلاعات رشته تحصیلی صحیح نیست.");

            RuleFor(c => c.MajorId).NotNull().When(c => c.DegreeLevel > DegreeLevel.Diploma)
                .WithMessage("اطلاعات رشته تحصیلی الزامی است.");

            RuleFor(c => c.UniversityId).NotNull().When(c => c.DegreeLevel > DegreeLevel.Diploma)
                .WithMessage("اطلاعات دانشگاه تحصیلی الزامی است.");

            RuleFor(c => c.StillEducating).Must(c => c).When(c => c.ToYear is null && c.DegreeLevel > DegreeLevel.Bachelor).WithMessage("سال پایان الزامی است.");
            RuleFor(c => c.StillEducating).Must(c => !c).When(c => c.ToYear is not null && c.DegreeLevel > DegreeLevel.Bachelor).WithMessage("سال پایان در حین تحصیل نمی‌تواند مقدار داشته باشد.");
        }
    }
}
