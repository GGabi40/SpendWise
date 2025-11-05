namespace SpendWise.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Relaciones
    // public ICollection<Transaction>? Transactions { get; set; }
    // public ICollection<Note>? Notes { get; set; }
}

