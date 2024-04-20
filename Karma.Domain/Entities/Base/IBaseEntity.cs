namespace Karma.Core.Entities.Base
{
    public interface IBaseEntity
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; }
    }
}
