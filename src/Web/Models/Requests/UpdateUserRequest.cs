using System.ComponentModel.DataAnnotations;

namespace SpendWise.Web.Models.Requests
{
    public class UpdateUserRequest
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(80)]
        public string Username { get; set; } = string.Empty;
        [Required, MaxLength(80)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(80)]
        public string Surname { get; set; } = string.Empty;
        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}