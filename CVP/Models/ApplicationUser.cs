using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CVP.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Nombre completo")]
        public required string FullName { get; set; }
    }
}
