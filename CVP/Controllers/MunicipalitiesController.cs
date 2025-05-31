using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVP.Data;
using CVP.Models;

namespace CVP.Controllers
{
    public class MunicipalitiesController : Controller
    {
        private readonly AppDbContext _context;
        public MunicipalitiesController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.Municipalities.OrderBy(m => m.Name).ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var m = await _context.Municipalities.FirstOrDefaultAsync(x => x.Id == id);
            if (m == null) return NotFound();
            return View(m);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Municipality model)
        {
            if (!ModelState.IsValid) return View(model);
            _context.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Municipio creado correctamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var m = await _context.Municipalities.FindAsync(id);
            if (m == null) return NotFound();
            return View(m);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Municipality model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);
            _context.Update(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Municipio editado correctamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var m = await _context.Municipalities.FirstOrDefaultAsync(x => x.Id == id);
            if (m == null) return NotFound();
            return View(m);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var m = await _context.Municipalities.FindAsync(id);
            _context.Municipalities.Remove(m);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Municipio eliminado correctamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
