using Karma.Application.Commands.Base;
using Karma.Application.Mappings;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;
using Karma.Core.Entities;
using Karma.Core.Enums;

namespace Karma.Application.Commands
{
    public class UpdateAboutMeCommand : IBaseCommand
    {
        public Guid? ImageId { get; set; }
        public required string MainJobTitle { get; set; }
        public string? Description { get; set; }
        public IEnumerable<SocialMediaCommand> SocialMedias { get; set; } = Enumerable.Empty<SocialMediaCommand>();

        public void Validate()
        {
            new UpdateAboutMeCommandValidator().Validate(this).RaiseExceptionIfRequired();
            foreach (var socialMedia in SocialMedias)
                socialMedia.Validate();
        }
    }

    public class SocialMediaCommand : IBaseCommand, IMapFrom<SocialMedia>
    {
        public SocialMediaType Type { get; set; }
        public required string Link { get; set; }

        public void Validate() => new SocialMediaCommandValidator().Validate(this).RaiseExceptionIfRequired();

    }
}
