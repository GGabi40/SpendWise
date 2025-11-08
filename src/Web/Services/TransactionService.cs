using SpendWise.Core.DTOs;
using SpendWise.Core.Entities;
using SpendWise.Core.Interfaces;
using SpendWise.Infrastructure.Repositories;

// Servicio para gestionar transacciones utilizando el repositorio TransactionRepository.
// Proporciona métodos CRUD y mapeo entre entidades y DTOs.

namespace SpendWise.Web.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUser;

        public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, ICurrentUserService currentUser)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _currentUser = currentUser;
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
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");

            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null || transaction.UserId != userId) return null;

            return TransactionDto.Create(transaction);
        }

        // Crea una nueva transacción.
        public async Task<Transaction> AddAsync(TransactionDto dto)
        {
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");

            var transaction = new Transaction(
                userId: userId,
                amount: dto.Amount,
                category: dto.Category,
                description: dto.Description
            );

            await _transactionRepository.AddAsync(transaction);
            return transaction;
        }

        // Actualiza una transacción existente.
        public async Task<bool> UpdateAsync(int id, TransactionDto dto)
        {
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");

            var existing = await _transactionRepository.GetByIdAsync(id);
            if (existing == null || existing.UserId != userId) return false;

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
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");
            
            var existing = await _transactionRepository.GetByIdAsync(id);
            if (existing == null || existing.UserId != userId) return false;

            await _transactionRepository.DeleteAsync(id);
            return true;
        }
    }
}
