using SpendWise.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpendWise.Core.Interfaces
{
    public interface INoteRepository
    {
        Task<List<Note>> GetAllAsync();
        Task<Note?> GetByIdAsync(int id);
        Task<List<Note>> GetByUserIdAsync(int userId);
        Task AddAsync(Note note);
        Task UpdateAsync(Note note);
        Task DeleteAsync(int id);
    }
}
