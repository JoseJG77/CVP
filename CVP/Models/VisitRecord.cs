using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CVP.Models
{
    public class VisitRecord
    {
        public int Id { get; set; }

        public int ProcedureId { get; set; }
        [ValidateNever]
        public Procedure? Procedure { get; set; }

        public int ApplicantId { get; set; }
        [ValidateNever]
        public Applicant? Applicant { get; set; }

        public int MunicipalityId { get; set; }
        [ValidateNever]
        public Municipality? Municipality { get; set; }

        public DateTime Date { get; set; }
        public char Gender { get; set; }
    }
}
