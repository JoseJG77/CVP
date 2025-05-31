using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVP.Data;
using CVP.Models;

namespace CVP.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly AppDbContext _context;
        public ApplicantsController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.Applicants.OrderBy(a => a.Name).ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var a = await _context.Applicants.FirstOrDefaultAsync(x => x.Id == id);
            if (a == null) return NotFound();
            return View(a);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Applicant model)
        {
            if (!ModelState.IsValid) return View(model);
            _context.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Solicitante creado correctamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var a = await _context.Applicants.FindAsync(id);
            if (a == null) return NotFound();
            return View(a);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Applicant model)
        {
            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);
            _context.Update(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Solicitante editado correctamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var a = await _context.Applicants.FirstOrDefaultAsync(x => x.Id == id);
            if (a == null) return NotFound();
            return View(a);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var a = await _context.Applicants.FindAsync(id);
            _context.Applicants.Remove(a);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Solicitante eliminado correctamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
