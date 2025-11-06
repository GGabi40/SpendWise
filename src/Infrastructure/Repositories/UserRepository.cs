using Microsoft.EntityFrameworkCore;
using SpendWise.Core.Entities;
using Infrastructure.Data;
using SpendWise.Core.Interfaces;


// Repositorio para la entidad Usuario : basado en DbContext
// Implementa los metodos definidos en la interfaz IUserRepository para el CRUD de usuarios

namespace SpendWise.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Obtener usuario por username
    public User? GetUserByUsername(string username)
    {
        return _context.Users.SingleOrDefault(u => u.Username == username);
    }

    public User? GetById(int id)
    {
        return _context.Users.SingleOrDefault(u => u.Id == id);
    }

    // Crear usuario
    public User Add(User entity)
    {
        _context.Users.Add(entity);
        return entity;
    }

    // Actualiza info de usuario
    public void Update(User entity)
    {
        _context.Users.Update(entity);
    }

    public void Delete(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
            _context.Users.Remove(user);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}
