using Microsoft.EntityFrameworkCore;
using ToursEcologicos.Application.Interfaces;
using ToursEcologicos.Domain.Entities;

namespace ToursEcologicos.Infrastructure.Data
{
    public class ReservaService : IReservaService
    {
        private readonly AppDbContext _context;

        public ReservaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reserva>> ObtenerTodasAsync()
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.ProgramacionTour)
                .ThenInclude(p => p!.Tour)
                .ToListAsync();
        }

        public async Task<Reserva?> ObtenerPorIdAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.ProgramacionTour)
                .ThenInclude(p => p!.Tour)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> CrearReservaAsync(Reserva reserva)
        {
            var programacion = await _context.ProgramacionesTour
                .FirstOrDefaultAsync(p => p.Id == reserva.ProgramacionTourId);

            if (programacion == null)
                return false;

            if (programacion.CuposDisponibles < reserva.CantidadPersonas)
                return false;

            programacion.CuposDisponibles -= reserva.CantidadPersonas;
            reserva.FechaReserva = DateTime.Now;
            reserva.Estado = "Activa";

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelarReservaAsync(int id)
        {
            var reserva = await _context.Reservas
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null || reserva.Estado == "Cancelada")
                return false;

            var programacion = await _context.ProgramacionesTour
                .FirstOrDefaultAsync(p => p.Id == reserva.ProgramacionTourId);

            if (programacion == null)
                return false;

            programacion.CuposDisponibles += reserva.CantidadPersonas;
            reserva.Estado = "Cancelada";

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task EliminarAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }
        }
    }
}