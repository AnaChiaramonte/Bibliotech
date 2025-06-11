using Biblioteca.Models;
using Bibliotech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bibliotech.Data
{
    public class BibliotechDBContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public BibliotechDBContext(DbContextOptions<BibliotechDBContext> options) : base(options)
        {
        }
        public DbSet<Avaliacao> avaliacoes { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Livros> livros { get; set; }
        public DbSet<ProgressoLeitura> progressos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Avaliacao>().ToTable("Avaliacoes");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Livros>().ToTable("Livros");
            modelBuilder.Entity<ProgressoLeitura>().ToTable("Progressos");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");


            Guid AdminGuid = Guid.NewGuid();
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = AdminGuid.ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Leitor",
                    NormalizedName = "LEITOR"
                }
            );

            Guid UserGuid = Guid.NewGuid();
            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = UserGuid.ToString(),
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = UserGuid.ToString(),
                    RoleId = AdminGuid.ToString()
                }
            );
        }
    }
}
