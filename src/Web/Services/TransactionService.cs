using SpendWise.Core.DTOs;
using SpendWise.Core.Entities;
using SpendWise.Infrastructure.Repositories;


// Servicio para gestionar transacciones utilizando TransactionRepository 
// Proporciona metodos para operaciones CRUD y mapeo entre entidades y DTOs 


namespace SpendWise.Web.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionService(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // Obtener todas las transacciones
        public async Task<List<TransactionDto>> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();

            return transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = t.Type,
                Category = t.Category,
                Date = t.Date,
                Description = t.Description,
                UserId = t.UserId
            }).ToList();
        }

        // Obtener una transaccion por ID
        public async Task<TransactionDto?> GetByIdAsync(int id)
        {
            var t = await _transactionRepository.GetByIdAsync(id);
            if (t == null) return null;

            return new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = t.Type,
                Category = t.Category,
                Date = t.Date,
                Description = t.Description,
                UserId = t.UserId
            };
        }

        // Obtener transacciones por usuario
        public async Task<List<TransactionDto>> GetByUserIdAsync(int userId)
        {
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);

            return transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Type = t.Type,
                Category = t.Category,
                Date = t.Date,
                Description = t.Description,
                UserId = t.UserId
            }).ToList();
        }

        // Crear nueva transaccion
        public async Task AddAsync(TransactionDto dto)
        {
            var transaction = new Transaction
            {
                Amount = dto.Amount,
                Type = dto.Type,
                Category = dto.Category,
                Date = dto.Date,
                Description = dto.Description,
                UserId = dto.UserId
            };

            await _transactionRepository.AddAsync(transaction);
        }

        // Actualizar transaccion existente
        public async Task<bool> UpdateAsync(int id, TransactionDto dto)
        {
            var existing = await _transactionRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Amount = dto.Amount;
            existing.Type = dto.Type;
            existing.Category = dto.Category;
            existing.Date = dto.Date;
            existing.Description = dto.Description;
            existing.UserId = dto.UserId;

            await _transactionRepository.UpdateAsync(existing);
            return true;
        }

        // Eliminar transaccion
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _transactionRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _transactionRepository.DeleteAsync(id);
            return true;
        }
    }
}
