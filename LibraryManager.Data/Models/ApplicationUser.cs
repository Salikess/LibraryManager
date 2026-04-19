using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string? Voornaam { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Achternaam { get; set; }
    }
}