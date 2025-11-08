using SpendWise.Core.Entities;

namespace SpendWise.Core.DTOs;

public record TransactionDto(
    decimal Amount,
    string Type,
    Category Category,
    DateTime Date,
    string? Description
)
{
    public static TransactionDto Create(Transaction transaction)
    {
        return new TransactionDto(
            transaction.Amount,
            transaction.Type,
            transaction.Category,
            transaction.Date,
            transaction.Description
        );
    }
}

