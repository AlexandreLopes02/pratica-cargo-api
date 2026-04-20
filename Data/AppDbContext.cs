using Microsoft.EntityFrameworkCore;
using PraticaCargo.Api.Models;

namespace PraticaCargo.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Motorista> Motoristas => Set<Motorista>();
        public DbSet<Empresa> Empresas => Set<Empresa>();
        public DbSet<Servico> Servicos => Set<Servico>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Motorista>()
                .HasIndex(m => m.Cpf)
                .IsUnique();

            modelBuilder.Entity<Empresa>()
                .HasIndex(e => e.Cnpj)
                .IsUnique();

            modelBuilder.Entity<Servico>()
                .HasOne(s => s.Empresa)
                .WithMany()
                .HasForeignKey(s => s.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Servico>()
                .HasOne(s => s.Motorista)
                .WithMany()
                .HasForeignKey(s => s.MotoristaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
