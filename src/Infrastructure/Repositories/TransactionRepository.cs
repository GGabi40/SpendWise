using Microsoft.EntityFrameworkCore;
using SpendWise.Core.Entities;
using Infrastructure.Data;


// Repositorio para la entidad Transaction: gestiona operaciones CRUD sobre la base de datos.

namespace SpendWise.Infrastructure.Repositories
{
    public class TransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtiene todas las transacciones, incluyendo el usuario asociado.
        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _context.Transactions
                .Include(t => t.User)
                .ToListAsync();
        }
        // Obtiene una transacción por su ID.
        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        // Agrega una nueva transacción a la base de datos.
        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
        // Actualiza una transacción existente.
        public async Task UpdateAsync(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
        }
        // Elimina una transacción por ID. 
        public async Task DeleteAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction is not null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}
