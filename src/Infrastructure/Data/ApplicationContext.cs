using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SpendWise.Core.Entities;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
