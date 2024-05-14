namespace Karma.Application.Services.Interfaces
{
    public interface IFileService
    {
        Task<(FileStream stream, string filename)> GetFileAsync(Guid id);
        Task<Guid> StoreFileAsync(MemoryStream stream, Guid userId);
    }
}
