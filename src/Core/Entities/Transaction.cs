using System.ComponentModel.DataAnnotations.Schema;

namespace SpendWise.Core.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty; // "Ingreso" o "Gasto"
        public Category Category { get; set; }
        
        [Column(TypeName = "Datetime")]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }

        // Relación con Usuario
        public User? User { get; private set; }
        public int UserId { get; private set; }

        public Transaction(int userId, decimal amount, Category category, string? description = null)
        {
            UserId = userId;
            Amount = amount;
            Category = category;
            Description = description;
        }

        private Transaction() { }
    }
}
