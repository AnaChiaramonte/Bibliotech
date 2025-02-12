using Bibliotech.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech.Data
{
    public class BibliotechDBContext : IdentityDbContext
    {
        public BibliotechDBContext(DbContextOptions<BibliotechDBContext> options) : base(options)
        {
        }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Livros> livros { get; set; }
        public DbSet<ProgressoLeitura> progressos { get; set; }
        public DbSet<Resenha> resenhas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Livros>().ToTable("Livros");
            modelBuilder.Entity<ProgressoLeitura>().ToTable("Progressos");
            modelBuilder.Entity<Resenha>().ToTable("Resenhas");
        }
    }
}
