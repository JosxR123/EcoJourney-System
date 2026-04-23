using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToursEcologicos.Domain.Entities;
using ToursEcologicos.Infrastructure.Data;

namespace ToursEcologicos.Web.Controllers
{
    public class ProgramacionToursController : Controller
    {
        private readonly AppDbContext _context;

        public ProgramacionToursController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var programaciones = _context.ProgramacionesTour
                .Include(p => p.Guia)
                .Include(p => p.Tour);

            return View(await programaciones.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var programacionTour = await _context.ProgramacionesTour
                .Include(p => p.Guia)
                .Include(p => p.Tour)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (programacionTour == null)
            {
                return NotFound();
            }

            return View(programacionTour);
        }

        public IActionResult Create()
        {
            ViewData["GuiaId"] = new SelectList(_context.Guias, "Id", "Nombre");
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgramacionTour programacionTour)
        {
            if (!ModelState.IsValid)
            {
                ViewData["GuiaId"] = new SelectList(_context.Guias, "Id", "Nombre", programacionTour.GuiaId);
                ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Nombre", programacionTour.TourId);
                return View(programacionTour);
            }

            _context.Add(programacionTour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var programacionTour = await _context.ProgramacionesTour.FindAsync(id);

            if (programacionTour == null)
            {
                return NotFound();
            }

            ViewData["GuiaId"] = new SelectList(_context.Guias, "Id", "Nombre", programacionTour.GuiaId);
            ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Nombre", programacionTour.TourId);
            return View(programacionTour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProgramacionTour programacionTour)
        {
            if (id != programacionTour.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["GuiaId"] = new SelectList(_context.Guias, "Id", "Nombre", programacionTour.GuiaId);
                ViewData["TourId"] = new SelectList(_context.Tours, "Id", "Nombre", programacionTour.TourId);
                return View(programacionTour);
            }

            _context.Update(programacionTour);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var programacionTour = await _context.ProgramacionesTour
                .Include(p => p.Guia)
                .Include(p => p.Tour)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (programacionTour == null)
            {
                return NotFound();
            }

            return View(programacionTour);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programacionTour = await _context.ProgramacionesTour.FindAsync(id);

            if (programacionTour != null)
            {
                _context.ProgramacionesTour.Remove(programacionTour);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}