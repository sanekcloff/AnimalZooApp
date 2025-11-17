using AnimalZooApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalZooApp.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Valier> Valiers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ZooAppDb;Trusted_Connection=True;");
        }

    }
}
