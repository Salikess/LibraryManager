using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Data.Models
{
    public class Boek
    {
        public int Id { get; set; }

        [Required]
        public string? Titel { get; set; }

        public string? ISBN { get; set; }

        public DateTime PublicatieDatum { get; set; }

        [Precision(10, 2)]
        public decimal Prijs { get; set; }

        public int AuteurId { get; set; }
        public int CategorieId { get; set; }

        public Auteur? Auteur { get; set; }
        public Categorie? Categorie { get; set; }
    }
}

