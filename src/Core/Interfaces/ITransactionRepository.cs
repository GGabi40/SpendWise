using SpendWise.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpendWise.Core.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<List<Transaction>> GetByUserIdAsync(int userId);
    }
}
