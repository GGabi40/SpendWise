namespace SpendWise.Core.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty; // "Ingreso" o "Gasto"
        public string Category { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }

        // Relación con Usuario
        public int UserId { get; set; }
    }
}
