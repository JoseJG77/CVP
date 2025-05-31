using System;
using System.Collections.Generic;

namespace CVP.ViewModels
{
    public class VisitData
    {
        public int Month { get; set; }
        public int Total { get; set; }
    }

    public class ProcedureReport
    {
        public string Name { get; set; } = "";
        public List<VisitData> Data { get; set; } = new();
    }

    public class CategoryReport
    {
        public string Category { get; set; } = "";
        public List<ProcedureReport> Procedures { get; set; } = new();
    }

    public class QuarterlyReportViewModel
    {
        public int Year { get; set; }
        public int Quarter { get; set; }
        public List<int> Months { get; set; } = new();
        public List<CategoryReport> Categories { get; set; } = new();
    }
}
