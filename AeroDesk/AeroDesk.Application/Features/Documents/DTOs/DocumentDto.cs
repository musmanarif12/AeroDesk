namespace AeroDesk.Application.Features.Documents.DTOs
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public int UploadedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}