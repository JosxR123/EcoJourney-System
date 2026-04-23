using ToursEcologicos.Domain.Entities;

namespace ToursEcologicos.Application.Interfaces
{
    public interface ICrudService<T> where T : BaseEntity
    {
        Task<List<T>> ObtenerTodosAsync();
        Task<T?> ObtenerPorIdAsync(int id);
        Task CrearAsync(T entidad);
        Task EditarAsync(T entidad);
        Task EliminarAsync(int id);
    }
}