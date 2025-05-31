using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CVP.Models;

namespace CVP.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<VisitRecord> VisitRecords { get; set; }
    }
}
