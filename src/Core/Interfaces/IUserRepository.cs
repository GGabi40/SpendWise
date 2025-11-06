using SpendWise.Core.Entities;
using System.Collections.Generic;

namespace SpendWise.Core.Interfaces;

// Define que operaciones debe implementar la capa Infrastructure
public interface IUserRepository
{
    User? GetUserByUsername(string username);
    User? GetById(int id);
    List<User> List();
    User Add(User entity);
    void Update(User entity);
    void Delete(int id);

    int SaveChanges();
}