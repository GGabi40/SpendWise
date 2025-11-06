using SpendWise.Core.Entities;

namespace SpendWise.Core.DTOs;

public record TransactionDto(
    int Id,
    decimal Amount,
    string Type,
    string Category,
    DateTime Date,
    string? Description
)
{
    public static TransactionDto Create(Transaction transaction)
    {
        return new TransactionDto(
            transaction.Id,
            transaction.Amount,
            transaction.Type,
            transaction.Category,
            transaction.Date,
            transaction.Description
        );
    }
}
