using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpendWise.Core.Entities;

public class User
{
    [Column(Order = 0)]
    public int Id { get; set; }

    [Required]
    [MaxLength(80)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string Surname { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(100)")]
    public required string Password { get; set; }

    // Relaciones
    // public ICollection<Transaction>? Transactions { get; set; }
    // public ICollection<Note>? Notes { get; set; }
}

