using Karma.Core.Entities.Base;
using Karma.Core.Enums;

namespace Karma.Core.Entities
{
    public class Language : IBaseEntity
    {
        public Language()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual required SystemLanguage SystemLanguage { get; set; }
        public LanguageLevel LanguageLevel { get; set; }
    }
}
