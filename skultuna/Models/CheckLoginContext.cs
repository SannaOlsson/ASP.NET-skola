using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

// DB håller koll på användarna
namespace skultuna.Models
{
    public class CheckLoginContext : DbContext
    {
        public DbSet<User> Users
        {
            get;
            set;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=userData.db");
        }
    }
}
