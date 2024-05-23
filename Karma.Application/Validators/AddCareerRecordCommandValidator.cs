using FluentValidation;
using Karma.Application.Commands;
using System.Globalization;

namespace Karma.Application.Validators
{
    internal class AddCareerRecordCommandValidator : AbstractValidator<AddCareerRecordCommand>
    {
        public AddCareerRecordCommandValidator()
        {
            var pc = new PersianCalendar();

            RuleFor(c => c.JobTitle).NotEmpty().WithMessage("عنوان شغلی الزامی است.");
            RuleFor(c => c.CompanyName).NotEmpty().WithMessage("نام سازمان الزامی است.");

            RuleFor(c => c.FromYear).GreaterThan(0).WithMessage("مقدار وارد شده برای سال شروع صحیح نیست.");

            RuleFor(c => c.FromYear).LessThanOrEqualTo(pc.GetYear(DateTime.Now)).WithMessage("مقدار وارد شده برای سال شروع صحیح نیست.");

            RuleFor(c => c.ToYear).LessThanOrEqualTo(pc.GetYear(DateTime.Now)).When(c => c.ToYear is not null)
                .WithMessage("مقدار وارد شده برای سال پایان صحیح نیست.");

            RuleFor(c => new DateTime(c.ToYear!.Value, c.ToMonth!.Value, 1, pc)).GreaterThan(t => new DateTime(t.FromYear, t.FromMonth, 1, pc))
                .When(c => c.ToYear is not null && c.ToMonth is not null)
                .WithMessage("سال و ماه پایان کار باید از سال و ماه شروع کار بزرگتر باشد.");

            RuleFor(c => c.CurrentJob).Must(c => c).When(c => c.ToYear is null).WithMessage("سال پایان الزامی است.");
            RuleFor(c => c.CurrentJob).Must(c => c).When(c => c.ToMonth is null).WithMessage("ماه پایان الزامی است.");
            RuleFor(c => c.CurrentJob).Must(c => !c).When(c => c.ToYear is not null).WithMessage("سال پایان نمی‌تواند مقدار داشته باشد.");
            RuleFor(c => c.CurrentJob).Must(c => !c).When(c => c.ToMonth is not null).WithMessage("ماه پایان نمی‌تواند مقدار داشته باشد.");
        }
    }
}
