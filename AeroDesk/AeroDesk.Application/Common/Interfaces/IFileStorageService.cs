namespace AeroDesk.Application.Common.Interfaces
{
    public interface IFileStorageService
    {
        
        Task<(string storedFileName, string filePath)> SaveFileAsync(
            Stream fileStream,
            string originalFileName,
            CancellationToken cancellationToken);

        Task<Stream> GetFileAsync(string filePath, CancellationToken cancellationToken);

        
        void DeleteFile(string filePath);
    }
}