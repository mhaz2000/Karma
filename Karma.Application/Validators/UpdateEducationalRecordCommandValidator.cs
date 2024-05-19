using FluentValidation;
using Karma.Application.Commands;
using System.Globalization;

namespace Karma.Application.Validators
{
    internal class UpdateEducationalRecordCommandValidator : AbstractValidator<UpdateEducationalRecordCommand>
    {
        public UpdateEducationalRecordCommandValidator()
        {
            var pc = new PersianCalendar();
            RuleFor(c => c.GPA).GreaterThanOrEqualTo(0).When(c => c.GPA is not null)
                .WithMessage("مقدار وارد شده برای معدل صحیح نیست.");
            
            RuleFor(c => c.GPA).LessThanOrEqualTo(20).When(c => c.GPA is not null)
                .WithMessage("مقدار وارد شده برای معدل صحیح نیست.");

            RuleFor(c => c.FromYear).GreaterThan(0).WithMessage("مقدار وارد شده برای سال شروع صحیح نیست.");

            RuleFor(c => c.FromYear).LessThanOrEqualTo(pc.GetYear(DateTime.Now)).WithMessage("مقدار وارد شده برای سال شروع صحیح نیست.");

            RuleFor(c => c.ToYear).LessThanOrEqualTo(pc.GetYear(DateTime.Now)).When(c => c.ToYear is not null)
                .WithMessage("مقدار وارد شده برای سال پایان صحیح نیست.");

            RuleFor(c => c.ToYear).GreaterThanOrEqualTo(c => c.FromYear).When(c => c.ToYear is not null)
                .WithMessage("سال شروع نمی‌تواند از سال پایان بزرگتر باشد.");

            RuleFor(c => c.StillEducating).Must(c => c).When(c => c.ToYear is null).WithMessage("سال پایان الزامی است.");
            RuleFor(c => c.StillEducating).Must(c => !c).When(c => c.ToYear is not null).WithMessage("سال پایان در حین تحصیل نمی‌تواند مقدار داشته باشد.");
        }
    }
}
