using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SpendWise.Core.Entities;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        // acá agreguen (dsp borren los comments):
        // NOTAS -> public DbSet<Note> Notes { get; set; }
        // TRANSACTION -> public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }


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
                    Name = "Jorge"
                }
            ];
            
            return result;
        }
    }
}
