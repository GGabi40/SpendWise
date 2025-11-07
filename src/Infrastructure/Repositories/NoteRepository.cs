using Microsoft.EntityFrameworkCore;
using SpendWise.Core.Entities;
using Infrastructure.Data;
using SpendWise.Core.Interfaces;


// Repositorio para la entidad Nota : basado en DbContext 
// Implementa los metodos definidos en la interfaz INoteRepository para el CRUD de notas


namespace SpendWise.Infrastructure.Repositories;

public class NoteRepository : GenericRepository<Note>, INoteRepository
{
    private readonly ApplicationDbContext _context;

    public NoteRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Note>> GetByUserIdAsync(int userId)
    {
        return await _context.Notes
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }
}