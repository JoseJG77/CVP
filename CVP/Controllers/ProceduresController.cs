using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVP.Data;
using CVP.Models;

namespace CVP.Controllers
{
    public class ProceduresController : Controller
    {
        private readonly AppDbContext _context;
        public ProceduresController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var list = await _context.Procedures
                                     .OrderBy(p => p.Category)
                                     .ThenBy(p => p.Name)
                                     .ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.Procedures
                                     .FirstOrDefaultAsync(p => p.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Procedure model)
        {
            if (!ModelState.IsValid) return View(model);
            _context.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Procedimiento creado correctamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.Procedures.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Procedure model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);
            _context.Update(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Procedimiento editado correctamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.Procedures
                                     .FirstOrDefaultAsync(p => p.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Procedures.FindAsync(id);
            _context.Procedures.Remove(item);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Procedimiento eliminado correctamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
