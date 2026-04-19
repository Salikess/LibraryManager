using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Data;
using LibraryManager.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManager.web.Controllers
{
   
    public class BoeksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoeksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Boeks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Boeken.Include(b => b.Auteur).Include(b => b.Categorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Boeks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boek = await _context.Boeken
                .Include(b => b.Auteur)
                .Include(b => b.Categorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boek == null)
            {
                return NotFound();
            }

            return View(boek);
        }

        // GET: Boeks/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "Id", "Achternaam");
            ViewData["CategorieId"] = new SelectList(_context.Categorieen, "Id", "Naam");
            return View();
        }

        // POST: Boeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titel,ISBN,PublicatieDatum,Prijs,AuteurId,CategorieId")] Boek boek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "Id", "Achternaam", boek.AuteurId);
            ViewData["CategorieId"] = new SelectList(_context.Categorieen, "Id", "Naam", boek.CategorieId);
            return View(boek);
        }

        // GET: Boeks/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boek = await _context.Boeken.FindAsync(id);
            if (boek == null)
            {
                return NotFound();
            }
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "Id", "Achternaam", boek.AuteurId);
            ViewData["CategorieId"] = new SelectList(_context.Categorieen, "Id", "Naam", boek.CategorieId);
            return View(boek);
        }

        // POST: Boeks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titel,ISBN,PublicatieDatum,Prijs,AuteurId,CategorieId")] Boek boek)
        {
            if (id != boek.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoekExists(boek.Id))
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
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "Id", "Achternaam", boek.AuteurId);
            ViewData["CategorieId"] = new SelectList(_context.Categorieen, "Id", "Naam", boek.CategorieId);
            return View(boek);
        }

        // GET: Boeks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boek = await _context.Boeken
                .Include(b => b.Auteur)
                .Include(b => b.Categorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boek == null)
            {
                return NotFound();
            }

            return View(boek);
        }

        // POST: Boeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boek = await _context.Boeken.FindAsync(id);
            if (boek != null)
            {
                _context.Boeken.Remove(boek);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoekExists(int id)
        {
            return _context.Boeken.Any(e => e.Id == id);
        }
    }
}
