using Karma.Application.Base;
using Karma.Application.Services.Interfaces;
using Karma.Core.Entities;
using Karma.Core.Repositories.Base;

namespace Karma.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(FileStream stream, string filename)> GetFileAsync(Guid id)
        {
            string filename = "File";
            var path = Directory.GetCurrentDirectory() + "\\FileStorage";
            var filePath = Path.Combine(path, $"{id}.dat");

            if (!File.Exists(filePath))
                throw new ManagedException("فایل مورد نظر یافت نشد.");

            var file = await _unitOfWork.FileRepository.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new ManagedException("فایل مورد نظر یافت نشد.");

            if (file.UploadedBy is not null && !string.IsNullOrEmpty(file.UploadedBy.FirstName))
                filename = $"{file.UploadedBy.FirstName} {file.UploadedBy.LastName}{file.Format}";

            return (new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read), filename);
        }

        public async Task<Guid> StoreFileAsync(MemoryStream file, Guid UserId, string filename)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(UserId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var path = Directory.GetCurrentDirectory() + "/FileStorage";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileId = Guid.NewGuid();
            await _unitOfWork.FileRepository.AddAsync(new UploadedFile() { UploadedBy = user, Id = fileId, Format = Path.GetExtension(filename) });

            var dir = Path.Combine(path, $"{fileId}.dat");

            using (var fileStream = new FileStream(dir, FileMode.CreateNew, FileAccess.Write, FileShare.Write))
            {
                file.Position = 0;
                await file.CopyToAsync(fileStream);
            }

            await _unitOfWork.CommitAsync();

            return fileId;
        }
    }
}
