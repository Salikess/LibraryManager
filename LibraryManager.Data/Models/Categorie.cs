using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Data.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [Required]
        public string? Naam { get; set; }

        public string? Beschrijving { get; set; }

        public List<Boek>? Boeken { get; set; }
    }
}
        