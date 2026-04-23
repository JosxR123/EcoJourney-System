using ToursEcologicos.Domain.Entities;

namespace ToursEcologicos.Application.Interfaces
{
    public interface IReservaService
    {
        Task<List<Reserva>> ObtenerTodasAsync();
        Task<Reserva?> ObtenerPorIdAsync(int id);
        Task<bool> CrearReservaAsync(Reserva reserva);
        Task<bool> CancelarReservaAsync(int id);
        Task EliminarAsync(int id);
    }
}