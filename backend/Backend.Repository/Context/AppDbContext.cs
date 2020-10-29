using Backend.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Backend.Repository.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Produtos { get; set; }
        public DbSet<AnaliseQuimica> AnalisesQuimicas {get; set;}

        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Analise Quimica

            modelBuilder.Entity<AnaliseQuimica>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<AnaliseQuimica>()
                .Property(p => p.CTC);

            modelBuilder.Entity<AnaliseQuimica>()
                .Property(p => p.SatBases);

            modelBuilder.Entity<AnaliseQuimica>()
                .Property(p => p.UserId);

            // Product
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnName("Nome");

            modelBuilder.Entity<Product>()
                .Property(p => p.Category)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnName("Categoria");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("nvarchar(5)")
                .HasMaxLength(5)
                .IsRequired()
                .HasColumnName("Preço");

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasColumnType("nvarchar(300)")
                .HasColumnName("Descrição");
            
            modelBuilder.Entity<Product>()
                .Property(p => p.ImageUrl)
                .HasColumnType("nvarchar(300)")
                .HasColumnName("Imagem");

        }
    }
}
