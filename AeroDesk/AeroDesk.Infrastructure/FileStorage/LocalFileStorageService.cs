using AeroDesk.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AeroDesk.Infrastructure.FileStorage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _rootPath;

        public LocalFileStorageService(IConfiguration configuration)
        {
            _rootPath = configuration["FileStorage:RootPath"]
                ?? throw new InvalidOperationException("FileStorage:RootPath is not configured.");

            if (!Directory.Exists(_rootPath))
            {
                Directory.CreateDirectory(_rootPath);
            }
        }

        public async Task<(string storedFileName, string filePath)> SaveFileAsync(
            Stream fileStream,
            string originalFileName,
            CancellationToken cancellationToken)
        {
            var extension = Path.GetExtension(originalFileName);
            var storedFileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(_rootPath, storedFileName);

            using (var outputStream = new FileStream(fullPath, FileMode.Create))
            {
                await fileStream.CopyToAsync(outputStream, cancellationToken);
            }

            return (storedFileName, fullPath);
        }

        public Task<Stream> GetFileAsync(string filePath, CancellationToken cancellationToken)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The requested file was not found on disk.", filePath);
            }

            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return Task.FromResult(stream);
        }

        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}