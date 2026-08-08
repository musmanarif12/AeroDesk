namespace AeroDesk.Application.Features.Documents.Queries.DownloadDocument
{
    public class DownloadDocumentResult
    {
        public Stream FileStream { get; set; } = null!;
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}