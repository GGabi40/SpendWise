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
    private List<Transaction> _allTransactions = new List<Transaction>();
    public IReadOnlyCollection<Transaction> Transaction => _allTransactions;
    private List<Note> _allNotes = new List<Note>();
    public IReadOnlyCollection<Note> Notes => _allNotes;

}

