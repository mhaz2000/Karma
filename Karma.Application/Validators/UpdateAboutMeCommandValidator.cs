using FluentValidation;
using Karma.Application.Commands;

namespace Karma.Application.Validators
{
    internal class UpdateAboutMeCommandValidator : AbstractValidator<UpdateAboutMeCommand>
    {
        public UpdateAboutMeCommandValidator()
        {
            RuleFor(c => c.MainJobTitle).NotEmpty().WithMessage("عنوان شغلی الزامی است.");
            RuleFor(c => c.SocialMedias).Must(c => c.DistinctBy(t => t.Type).Count() == c.Count()).WithMessage("تنها یک رکورد از هر شبکه مجازی می‌توانید ثبت کنید.");
        }
    }
    
    internal class SocialMediaCommandValidator : AbstractValidator<SocialMediaCommand>
    {
        public SocialMediaCommandValidator()
        {
            RuleFor(c=> c.Link).NotEmpty().WithMessage("آدرس شبکه اجتماعی الزامی است.");
        }
    }
}
