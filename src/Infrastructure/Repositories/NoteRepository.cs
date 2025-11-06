using Microsoft.EntityFrameworkCore;
using SpendWise.Core.Entities;
using Infrastructure.Data;
using SpendWise.Core.Interfaces;


// Repositorio para la entidad Nota : basado en DbContext 
// Implementa los metodos definidos en la interfaz INoteRepository para el CRUD de notas


namespace SpendWise.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;

        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todas las notas
        public async Task<List<Note>> GetAllAsync()
        {
            return await _context.Notes
                .Include(n => n.User)
                .ToListAsync();
        }

        // Obtener nota por ID
        public async Task<Note?> GetByIdAsync(int id)
        {
            return await _context.Notes
                .Include(n => n.User)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        // Obtener notas por usuario
        public async Task<List<Note>> GetByUserIdAsync(int userId)
        {
            return await _context.Notes
                .Where(n => n.UserId == userId)
                .Include(n => n.User)
                .ToListAsync();
        }

        // Crear nota
        public async Task AddAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
        }

        // Actualizar nota
        public async Task UpdateAsync(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }

        // Eliminar nota
        public async Task DeleteAsync(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }
    }
}
