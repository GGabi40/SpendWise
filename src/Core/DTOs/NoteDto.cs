namespace SpendWise.Core.DTOs
{
    public class NoteDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPinned { get; set; } = false;
    }
}