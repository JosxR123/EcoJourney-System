using Microsoft.EntityFrameworkCore;
using ToursEcologicos.Application.Interfaces;
using ToursEcologicos.Domain.Entities;

namespace ToursEcologicos.Infrastructure.Data
{
    public class CrudService<T> : ICrudService<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public CrudService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> ObtenerTodosAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> ObtenerPorIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task CrearAsync(T entidad)
        {
            _context.Set<T>().Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EditarAsync(T entidad)
        {
            _context.Set<T>().Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.Set<T>().FindAsync(id);
            if (entidad != null)
            {
                _context.Set<T>().Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}