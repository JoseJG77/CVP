using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVP.Data;
using CVP.ViewModels;

namespace CVP.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;
        public ReportsController(AppDbContext context) => _context = context;

        public IActionResult Index()
        {
            var now = DateTime.Now;
            var quarter = (now.Month - 1) / 3 + 1;
            return RedirectToAction(nameof(Quarterly),
                                    new { year = now.Year, quarter });
        }

        public IActionResult Quarterly(int year, int quarter)
        {
            var startMonth = (quarter - 1) * 3 + 1;
            var months = Enumerable.Range(startMonth, 3).ToList();

            var procedures = _context.Procedures
                                     .AsNoTracking()
                                     .OrderBy(p => p.Category)
                                     .ThenBy(p => p.Name)
                                     .ToList();

            var categories = procedures
                .GroupBy(p => p.Category)
                .Select(g => new CategoryReport
                {
                    Category = g.Key,
                    Procedures = g
                        .Select(p => new ProcedureReport
                        {
                            Name = p.Name,
                            Data = months
                                .Select(m => new VisitData
                                {
                                    Month = m,
                                    Total = _context.VisitRecords
                                                    .Count(v =>
                                                        v.ProcedureId == p.Id &&
                                                        v.Date.Year == year &&
                                                        v.Date.Month == m)
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();

            // 4) Construir el ViewModel
            var vm = new QuarterlyReportViewModel
            {
                Year = year,
                Quarter = quarter,
                Months = months,
                Categories = categories
            };

            return View(vm);
        }
    }
}
