using Microsoft.EntityFrameworkCore;
using ToursEcologicos.Domain.Entities;

namespace ToursEcologicos.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Guia> Guias => Set<Guia>();
        public DbSet<Ruta> Rutas => Set<Ruta>();
        public DbSet<Tour> Tours => Set<Tour>();
        public DbSet<ProgramacionTour> ProgramacionesTour => Set<ProgramacionTour>();
        public DbSet<Reserva> Reservas => Set<Reserva>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.Ruta)
                .WithMany()
                .HasForeignKey(t => t.RutaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgramacionTour>()
                .HasOne(p => p.Tour)
                .WithMany()
                .HasForeignKey(p => p.TourId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProgramacionTour>()
                .HasOne(p => p.Guia)
                .WithMany()
                .HasForeignKey(p => p.GuiaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany()
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.ProgramacionTour)
                .WithMany()
                .HasForeignKey(r => r.ProgramacionTourId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}