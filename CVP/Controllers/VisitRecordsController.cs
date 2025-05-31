using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVP.Data;
using CVP.Models;

namespace CVP.Controllers
{
    public class VisitRecordsController : Controller
    {
        private readonly AppDbContext _context;
        public VisitRecordsController(AppDbContext context) => _context = context;

        void PopulateDropDowns()
        {
            ViewBag.Procedures = _context.Procedures.OrderBy(p => p.Category).ThenBy(p => p.Name).ToList();
            ViewBag.Applicants = _context.Applicants.OrderBy(a => a.Name).ToList();
            ViewBag.Municipalities = _context.Municipalities.OrderBy(m => m.Name).ToList();
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.VisitRecords
                .Include(v => v.Procedure)
                .Include(v => v.Applicant)
                .Include(v => v.Municipality)
                .OrderByDescending(v => v.Date)
                .ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var vr = await _context.VisitRecords
                .Include(v => v.Procedure)
                .Include(v => v.Applicant)
                .Include(v => v.Municipality)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (vr == null) return NotFound();
            return View(vr);
        }

        public IActionResult Create()
        {
            PopulateDropDowns();
            return View(new VisitRecord { Date = DateTime.Now });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VisitRecord vr)
        {
            vr.Date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                PopulateDropDowns();
                return View(vr);
            }
            _context.Add(vr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var vr = await _context.VisitRecords.FindAsync(id);
            if (vr == null) return NotFound();
            PopulateDropDowns();
            return View(vr);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VisitRecord vr)
        {
            if (id != vr.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                PopulateDropDowns();
                return View(vr);
            }
            _context.Update(vr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var vr = await _context.VisitRecords
                .Include(v => v.Procedure)
                .Include(v => v.Applicant)
                .Include(v => v.Municipality)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (vr == null) return NotFound();
            return View(vr);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vr = await _context.VisitRecords.FindAsync(id);
            _context.VisitRecords.Remove(vr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
