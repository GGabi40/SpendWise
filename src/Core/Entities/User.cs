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
    public string Password { get; private set; } = null!;

    // Relaciones
    private List<Transaction> _allTransactions = new List<Transaction>();
    public IReadOnlyCollection<Transaction> Transaction => _allTransactions;
    private List<Note> _allNotes = new List<Note>();
    public IReadOnlyCollection<Note> Notes => _allNotes;

    public User(string username, string email, string name, string surname, string password)
    {
        Username = username;
        Email = email;
        Name = name;
        Surname = surname;
        Password = password;
    }

    public void AddNote(string title, string content, bool pinned = false)
    {
        var note = new Note(this.Id, title, content, pinned);
        _allNotes.Add(note);
    }

    public void UpdateProfile(string username, string name, string surname, string email)
    {
        Username = username;
        Name = name;
        Surname = surname;
        Email = email;
    }

    public void ChangePassword(string password)
    {
        Password = password;
    }

    protected User() { }
}

