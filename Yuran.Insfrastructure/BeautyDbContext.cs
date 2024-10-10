using Microsoft.EntityFrameworkCore;
using Yuran.Domain.Models;
using System; 

namespace Yuran.Insfrastructure
{
    public class BeautyDbContext : DbContext
    {
        public BeautyDbContext()
        {
            // Garante que o banco de dados seja criado se não existir
            this.Database.EnsureCreated();
            var path = "C:\\Users\\Yuran Mendes\\source\\repos\\EF - SQLITE - UWP\\Yuran.Insfrastructure\\Database\\1";
            DbPath = System.IO.Path.Combine(path, "Yuran-TRAIL.db");
        }
        public string DbPath { get; private set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PostalCode> PostalCodes {get;set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Destino> Destinos { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<EntryProduct> EntryProducts { get; set; }
        public DbSet<OutProduct> OutProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {DbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<User>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<User>().Property(x => x.Name).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<PostalCode>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<PostalCode>().Property(x => x.Id).IsRequired().HasMaxLength(2);

            modelBuilder.Entity<Fornecedor>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Fornecedor>().Property(x => x.Name).IsRequired().HasMaxLength(256);

            modelBuilder.Entity<Destino>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Destino>().Property(x => x.Id).IsRequired().HasMaxLength(2);

            modelBuilder.Entity<Permission>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Permission>().Property(x => x.Id).IsRequired().HasMaxLength(2);

            modelBuilder.Entity<EntryProduct>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<EntryProduct>().Property(x => x.Id).IsRequired().HasMaxLength(2);

    }
    }
}
