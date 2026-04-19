using LibraryManager.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using AppDbContext = LibraryManager.Data.ApplicationDbContext;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManager.web.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var boeken = await _context.Boeken
                .Include(b => b.Auteur)
                .Include(b => b.Categorie)
                .ToListAsync();

            var auteurs = await _context.Auteurs.ToListAsync();
            var categories = await _context.Categorieen.ToListAsync();

            ViewBag.Boeken = boeken;
            ViewBag.Auteurs = auteurs;
            ViewBag.Categories = categories;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}