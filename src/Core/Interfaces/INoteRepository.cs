using SpendWise.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpendWise.Core.Interfaces
{
    public interface INoteRepository : IGenericRepository<Note>
    {
        Task<List<Note>> GetByUserIdAsync(int userId);
    }
}
