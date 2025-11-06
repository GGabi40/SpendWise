namespace SpendWise.Core.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty; // "Ingreso" o "Gasto"
        public Category Category { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }

        // Relación con Usuario
        public User? User { get; set; }
    }
}
