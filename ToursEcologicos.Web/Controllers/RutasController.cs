using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToursEcologicos.Domain.Entities;
using ToursEcologicos.Infrastructure.Data;

namespace ToursEcologicos.Web.Controllers
{
    public class RutasController : Controller
    {
        private readonly AppDbContext _context;

        public RutasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Rutas.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var ruta = await _context.Rutas.FirstOrDefaultAsync(r => r.Id == id);

            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ruta ruta)
        {
            if (!ModelState.IsValid)
            {
                return View(ruta);
            }

            _context.Rutas.Add(ruta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ruta = await _context.Rutas.FindAsync(id);

            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ruta ruta)
        {
            if (id != ruta.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(ruta);
            }

            _context.Rutas.Update(ruta);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ruta = await _context.Rutas.FirstOrDefaultAsync(r => r.Id == id);

            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ruta = await _context.Rutas.FindAsync(id);

            if (ruta != null)
            {
                _context.Rutas.Remove(ruta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}