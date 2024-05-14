using FluentValidation;
using Karma.Application.Commands;

namespace Karma.Application.Validators
{
    internal class UpdateAboutMeCommandValidator : AbstractValidator<UpdateAboutMeCommand>
    {
        public UpdateAboutMeCommandValidator()
        {
            RuleFor(c => c.MainJobTitle).NotEmpty().WithMessage("عنوان شغلی الزامی است.");
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
