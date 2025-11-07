using Microsoft.EntityFrameworkCore;
using SpendWise.Core.Entities;
using Infrastructure.Data;
using SpendWise.Core.Interfaces;


// Repositorio para la entidad Transaction: gestiona operaciones CRUD sobre la base de datos.

namespace SpendWise.Infrastructure.Repositories;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Transaction>> GetByUserIdAsync(int userId)
    {
        return await _context.Transactions
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }
}
