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

        // Si necesitas agregar otras entidades (por ejemplo, para otros módulos), puedes hacerlo aquí:
        // public DbSet<OtroModelo> Otros { get; set; }
    }
}
