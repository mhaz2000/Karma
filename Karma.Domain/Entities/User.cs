using Karma.Core.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Karma.Core.Entities
{
    public sealed class User : IdentityUser<Guid>, IBaseEntity
    {
        public User()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public User(string firstName, string lastName, string nationalCode)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
        }

        public DateTime CreatedAt { get; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
    }
}
