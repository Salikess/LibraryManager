using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Data.Models
{
    public class Auteur
    {
        public int Id { get; set; }

        [Required]
        public string? Voornaam { get; set; }

        [Required]
        public string? Achternaam { get; set; }
    
    public List<Boek>? Boeken { get; set; }
    }   }