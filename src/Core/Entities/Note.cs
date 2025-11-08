using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SpendWise.Core.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPinned { get; set; } = false;
        
        [Column(TypeName = "Datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relación con Usuario
        [JsonIgnore]
        public User? User { get; private set; }
        public int UserId { get; private set; }

        public Note(int userId, string title, string content, bool isPinned = false)
        {
            UserId = userId;
            Title = title;
            Content = content;
            IsPinned = isPinned;
        }

        // Lo utiliza el EF
        public Note() { }

        // fijar/desfijar la nota
        public void Pin() => IsPinned = true;
        public void Unpin() => IsPinned = false;
    }
}
