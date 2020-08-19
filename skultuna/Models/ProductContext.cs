using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

// DB håller koll på produkterna
namespace skultuna.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products
        {
            get;
            set;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=productData.db");
        }
    }
}
