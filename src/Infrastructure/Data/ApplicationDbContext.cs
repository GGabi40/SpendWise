using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SpendWise.Core.Entities;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Note> Notes { get; set; } 


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(CreateUserDataSeed());

            base.OnModelCreating(modelBuilder);
        }

        private User[] CreateUserDataSeed()
        {
            User[] result;

            result = [
                new User
                {
                    Username = "admin",
                    Email = "admin@example.com",
                    Id = 1,
                    Name = "Admin",
                    Surname = "Admin",
                    Password = "1234"
                }
            ];
            
            return result;
        }
    }
}
