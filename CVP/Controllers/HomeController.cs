using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVP.Data;
using CVP.ViewModels;

namespace CVP.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            if (!(User.Identity?.IsAuthenticated ?? false))
                return View();

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var visitsToday = await _context.VisitRecords
                .Where(v => v.Date >= today && v.Date < tomorrow)
                .Include(v => v.Procedure)
                .Include(v => v.Municipality)
                .ToListAsync();

            var total = visitsToday.Count;

            var byProc = visitsToday
                .GroupBy(v => v.Procedure.Name)
                .Select(g => new NameValue { Name = g.Key, Value = g.Count() })
                .ToList();

            var byMun = visitsToday
                .GroupBy(v => v.Municipality.Name)
                .Select(g => new NameValue { Name = g.Key, Value = g.Count() })
                .ToList();

            var byHour = Enumerable.Range(0, 24)
                .Select(h => new NameValue
                {
                    Name = $"{h:D2}:00",
                    Value = visitsToday.Count(v => v.Date.Hour == h)
                })
                .ToList();

            var vm = new DashboardViewModel
            {
                TotalVisitsToday = total,
                VisitsByProcedure = byProc,
                VisitsByMunicipality = byMun,
                VisitsByHour = byHour
            };

            return View(vm);
        }
    }
}
