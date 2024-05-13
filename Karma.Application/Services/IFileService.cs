namespace Karma.Application.Services
{
    public interface IFileService
    {
        Task<Guid> StoreFile(MemoryStream stream, Guid userId);
    }
}
