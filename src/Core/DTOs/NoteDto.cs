namespace SpendWise.Core.DTOs
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPinned { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relación con Usuario
        public int UserId { get; set; }
    }
}