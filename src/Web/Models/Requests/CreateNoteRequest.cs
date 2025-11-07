using System.ComponentModel.DataAnnotations;

namespace SpendWise.Web.Models.Requests
{
    public record CreateNoteRequest
    (
        [Required]
        string Title,
        [Required]
        string Content,
        
        bool IsPinned
    );
}
