using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToursEcologicos.Domain.Entities;
using ToursEcologicos.Infrastructure.Data;

namespace ToursEcologicos.Web.Controllers
{
    public class GuiasController : Controller
    {
        private readonly AppDbContext _context;

        public GuiasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Guias.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var guia = await _context.Guias.FirstOrDefaultAsync(g => g.Id == id);

            if (guia == null)
            {
                return NotFound();
            }

            return View(guia);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guia guia)
        {
            if (!ModelState.IsValid)
            {
                return View(guia);
            }

            _context.Guias.Add(guia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var guia = await _context.Guias.FindAsync(id);

            if (guia == null)
            {
                return NotFound();
            }

            return View(guia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Guia guia)
        {
            if (id != guia.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(guia);
            }

            _context.Guias.Update(guia);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var guia = await _context.Guias.FirstOrDefaultAsync(g => g.Id == id);

            if (guia == null)
            {
                return NotFound();
            }

            return View(guia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guia = await _context.Guias.FindAsync(id);

            if (guia != null)
            {
                _context.Guias.Remove(guia);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}