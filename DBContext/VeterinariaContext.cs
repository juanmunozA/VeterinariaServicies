using Microsoft.EntityFrameworkCore;
using Veterinaria.Clases;

namespace Veterinaria.DBContext
{
    public class VeterinariaContext : DbContext
    {
        public VeterinariaContext(DbContextOptions<VeterinariaContext> options)
            : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Raza> Razas { get; set; }
        public DbSet<HistorialClinico> HistorialesClinicos { get; set; }
        public DbSet<Veterinario> Veterinarios { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<FormulaMedicamento> FormulaMedicamentos { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relación entre Mascota y Cliente
            modelBuilder.Entity<Mascota>()
                .HasOne(m => m.Cliente)
                .WithMany(c => c.Mascotas)
                .HasForeignKey(m => m.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); // Evitar cascada

            // Configurar relación entre Mascota y Raza
            modelBuilder.Entity<Mascota>()
                .HasOne(m => m.Raza)
                .WithMany(r => r.Mascotas)
                .HasForeignKey(m => m.RazaId)
                .OnDelete(DeleteBehavior.Restrict); // Evitar cascada

            // 🔹 Evitar ciclos de cascada en toda la jerarquía
            modelBuilder.Entity<HistorialClinico>()
                .HasOne(h => h.Mascota)
                .WithMany()
                .HasForeignKey(h => h.MascotaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistorialClinico>()
                .HasOne(h => h.Cliente)
                .WithMany()
                .HasForeignKey(h => h.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistorialClinico>()
                .HasOne(h => h.Formula)
                .WithMany()
                .HasForeignKey(h => h.FormulaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistorialClinico>()
                .HasOne(h => h.Veterinario)
                .WithMany()
                .HasForeignKey(h => h.VeterinarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
