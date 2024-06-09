using Karma.Application.Commands.Base;
using Karma.Application.Validators;
using Karma.Application.Validators.Extensions;
using Karma.Core.Enums;

namespace Karma.Application.Commands
{
    public class UpdateBasicInfoCommand : IBaseCommand
    {

        public required string FirstName { get; set; } 
        public required string LastName { get; set; } 
        public MaritalStatus MaritalStatus { get; set; }
        public MilitaryServiceStatus MilitaryServiceStatus { get; set; }
        public Gender Gender { get; set; }
        public required string City { get; set; }
        public DateTime BirthDate { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public string? Email { get; set; }

        public void Validate() => new UpdateBasicInfoCommandValidator().Validate(this).RaiseExceptionIfRequired();

    }
}
