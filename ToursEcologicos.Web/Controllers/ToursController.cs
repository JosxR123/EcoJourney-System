using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToursEcologicos.Domain.Entities;
using ToursEcologicos.Infrastructure.Data;

namespace ToursEcologicos.Web.Controllers
{
    public class ToursController : Controller
    {
        private readonly AppDbContext _context;

        public ToursController(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var tours = _context.Tours.Include(t => t.Ruta);
            return View(await tours.ToListAsync());
        }

        // DETALLES
        public async Task<IActionResult> Details(int id)
        {
            var tour = await _context.Tours
                .Include(t => t.Ruta)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tour == null) return NotFound();

            return View(tour);
        }

        // CREAR
        public IActionResult Create()
        {
            ViewData["RutaId"] = new SelectList(_context.Rutas, "Id", "NombreRuta");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tour tour)
        {
            if (!ModelState.IsValid)
            {
                ViewData["RutaId"] = new SelectList(_context.Rutas, "Id", "NombreRuta", tour.RutaId);
                return View(tour);
            }

            _context.Add(tour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // EDITAR
        public async Task<IActionResult> Edit(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null) return NotFound();

            ViewData["RutaId"] = new SelectList(_context.Rutas, "Id", "NombreRuta", tour.RutaId);
            return View(tour);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Tour tour)
        {
            if (id != tour.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["RutaId"] = new SelectList(_context.Rutas, "Id", "NombreRuta", tour.RutaId);
                return View(tour);
            }

            _context.Update(tour);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ELIMINAR
        public async Task<IActionResult> Delete(int id)
        {
            var tour = await _context.Tours
                .Include(t => t.Ruta)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tour == null) return NotFound();

            return View(tour);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tour = await _context.Tours.FindAsync(id);

            if (tour != null)
            {
                _context.Tours.Remove(tour);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}