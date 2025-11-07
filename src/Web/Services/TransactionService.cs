using SpendWise.Core.DTOs;
using SpendWise.Core.Entities;
using SpendWise.Infrastructure.Repositories;

// Servicio para gestionar transacciones utilizando el repositorio TransactionRepository.
// Proporciona métodos CRUD y mapeo entre entidades y DTOs.

namespace SpendWise.Web.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionService(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        // Obtiene todas las transacciones.
        public async Task<List<TransactionDto>> GetAllAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions.Select(TransactionDto.Create).ToList();
        }
        // Obtiene una transacción por su ID.
        public async Task<TransactionDto?> GetByIdAsync(int id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            return transaction is null ? null : TransactionDto.Create(transaction);
        }

        // Crea una nueva transacción.
        public async Task AddAsync(TransactionDto dto)
        {
            var transaction = new Transaction(
                amount: dto.Amount,
                category: dto.Category,
                description: dto.Description
            );
            await _transactionRepository.AddAsync(transaction);
        }

        // Actualiza una transacción existente.
        public async Task<bool> UpdateAsync(int id, TransactionDto dto)
        {
            var existing = await _transactionRepository.GetByIdAsync(id);
            if (existing is null)
                return false;

            existing.Amount = dto.Amount;
            existing.Type = dto.Type;
            existing.Category = dto.Category; // Actualiza el enum
            existing.Date = dto.Date;
            existing.Description = dto.Description;

            await _transactionRepository.UpdateAsync(existing);
            return true;
        }

        // Elimina una transacción por ID.
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _transactionRepository.GetByIdAsync(id);
            if (existing is null)
                return false;

            await _transactionRepository.DeleteAsync(id);
            return true;
        }
    }
}
