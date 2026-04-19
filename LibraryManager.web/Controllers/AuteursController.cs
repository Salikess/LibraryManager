using System.Linq;
using System.Threading.Tasks;
using LibraryManager.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppDbContext = LibraryManager.Data.ApplicationDbContext;

namespace LibraryManager.web.Controllers
{
    public class AuteursController : Controller
    {
        private readonly AppDbContext _context;

        public AuteursController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Auteurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Auteurs.ToListAsync());
        }

        // GET: Auteurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auteur = await _context.Auteurs
                .FirstOrDefaultAsync(m => m.Id == id);

            if (auteur == null)
            {
                return NotFound();
            }

            return View(auteur);
        }

        // GET: Auteurs/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Auteurs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Voornaam,Achternaam")] Auteur auteur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(auteur);
        }

        // GET: Auteurs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auteur = await _context.Auteurs.FindAsync(id);
            if (auteur == null)
            {
                return NotFound();
            }

            return View(auteur);
        }

        // POST: Auteurs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Voornaam,Achternaam")] Auteur auteur)
        {
            if (id != auteur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuteurExists(auteur.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(auteur);
        }

        // GET: Auteurs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auteur = await _context.Auteurs
                .FirstOrDefaultAsync(m => m.Id == id);

            if (auteur == null)
            {
                return NotFound();
            }

            return View(auteur);
        }

        // POST: Auteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auteur = await _context.Auteurs.FindAsync(id);

            if (auteur != null)
            {
                _context.Auteurs.Remove(auteur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuteurExists(int id)
        {
            return _context.Auteurs.Any(e => e.Id == id);
        }
    }
}