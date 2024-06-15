using Karma.Core.Entities.Base;

namespace Karma.Core.Entities
{
    public class UploadedFile : IBaseEntity
    {
        public UploadedFile()
        {
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public required string Format { get; set; }
        public virtual required User UploadedBy { get; set; }
    }
}
