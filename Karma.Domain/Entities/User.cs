using Karma.Core.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Karma.Core.Entities
{
    public class User : IdentityUser<Guid>, IBaseEntity
    {
        public User()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            SecurityStamp = Guid.NewGuid().ToString();
        }

        public User(string username, string firstName, string lastName, string nationalCode)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            SecurityStamp = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            UserName = username;
        }

        public DateTime CreatedAt { get; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
    }
}
