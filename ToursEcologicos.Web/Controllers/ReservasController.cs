using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToursEcologicos.Domain.Entities;
using ToursEcologicos.Infrastructure.Data;

namespace ToursEcologicos.Web.Controllers
{
    public class ReservasController : Controller
    {
        private readonly AppDbContext _context;

        public ReservasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reservas = _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.ProgramacionTour)
                .ThenInclude(p => p.Tour);

            return View(await reservas.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.ProgramacionTour)
                .ThenInclude(p => p.Tour)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["ProgramacionTourId"] = new SelectList(_context.ProgramacionesTour, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", reserva.ClienteId);
                ViewData["ProgramacionTourId"] = new SelectList(_context.ProgramacionesTour, "Id", "Id", reserva.ProgramacionTourId);
                return View(reserva);
            }

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", reserva.ClienteId);
            ViewData["ProgramacionTourId"] = new SelectList(_context.ProgramacionesTour, "Id", "Id", reserva.ProgramacionTourId);
            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", reserva.ClienteId);
                ViewData["ProgramacionTourId"] = new SelectList(_context.ProgramacionesTour, "Id", "Id", reserva.ProgramacionTourId);
                return View(reserva);
            }

            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.ProgramacionTour)
                .ThenInclude(p => p.Tour)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}