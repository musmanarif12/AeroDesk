using AeroDesk.Domain.Common;

namespace AeroDesk.Domain.Entities
{
    public class Document : BaseEntity
    {
        public string FileName { get; set; } = string.Empty;
        public string StoredFileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }

        public string EntityType { get; set; } = string.Empty; // "Passenger", "Booking", "CheckIn", "Airline", "Aircraft"
        public int EntityId { get; set; }

        public int UploadedByUserId { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation Property
        public User UploadedByUser { get; set; } = null!;
    }
}