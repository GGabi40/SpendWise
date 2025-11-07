using SpendWise.Core.Entities;
using System.Collections.Generic;

namespace SpendWise.Core.Interfaces;

// Define que operaciones debe implementar la capa Infrastructure
public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByUsernameAsync(string username);
}