using System.Collections.Generic;

namespace CVP.ViewModels
{
    public class NameValue
    {
        public string Name { get; set; } = "";
        public int Value { get; set; }
    }

    public class DashboardViewModel
    {
        public int TotalVisitsToday { get; set; }
        public List<NameValue> VisitsByProcedure { get; set; } = new();
        public List<NameValue> VisitsByMunicipality { get; set; } = new();
        public List<NameValue> VisitsByHour { get; set; } = new();
    }
}
