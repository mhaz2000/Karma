using Karma.Application.Base;
using Karma.Application.Services.Interfaces;
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
            string filename = "Image";
            var path = Directory.GetCurrentDirectory() + "\\FileStorage";
            var filePath = Path.Combine(path, $"{id}.dat");

            if (!File.Exists(filePath))
                throw new ManagedException("فایل مورد نظر یافت نشد.");

            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.ImageId == id);
            if (user is not null && string.IsNullOrEmpty(user.FirstName))
                filename = $"{user.FirstName} {user.LastName}";

            return (new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read), filename);
        }

        public async Task<Guid> StoreFileAsync(MemoryStream file, Guid UserId)
        {
            var user = await _unitOfWork.UserRepository.GetActiveUserByIdAsync(UserId);
            if (user is null)
                throw new ManagedException("کاربر مورد نظر یافت نشد.");

            var path = Directory.GetCurrentDirectory() + "\\FileStorage";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileId = Guid.NewGuid();
            var dir = Path.Combine(path, $"{fileId}.dat");

            using (var fileStream = new FileStream(dir, FileMode.CreateNew, FileAccess.Write, FileShare.Write))
            {
                file.Position = 0;
                await file.CopyToAsync(fileStream);
            }

            user.ImageId = fileId;

            await _unitOfWork.CommitAsync();

            return fileId;
        }
    }
}
