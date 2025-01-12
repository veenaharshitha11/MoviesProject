﻿//Veena Harshitha Gandhe
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesProject.Data;
using MoviesProject.Models;

namespace MoviesProject.Controllers
{
    public class CastsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CastsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Casts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cast.Include(c => c.Actor).Include(c => c.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Casts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cast == null)
            {
                return NotFound();
            }

            var cast = await _context.Cast
                .Include(c => c.Actor)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.CastID == id);
            if (cast == null)
            {
                return NotFound();
            }

            return View(cast);
        }
        [Authorize(Roles = "Administrator")]
        // GET: Casts/Create
        public IActionResult Create()
        {
            ViewData["ActorID"] = new SelectList(_context.Actors, "ID", "LastName");
            ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "Title");
            return View();
        }

        // POST: Casts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CastID,RolePlayed,ActorID,MovieID")] Cast cast)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorID"] = new SelectList(_context.Actors, "ID", "Discriminator", cast.ActorID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "MovieID", cast.MovieID);
            return View(cast);
        }
        [Authorize(Roles = "Administrator")]
        // GET: Casts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cast == null)
            {
                return NotFound();
            }

            var cast = await _context.Cast.FindAsync(id);
            if (cast == null)
            {
                return NotFound();
            }
            ViewData["ActorID"] = new SelectList(_context.Actors, "ID", "LastName", cast.ActorID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "Title", cast.MovieID);
            return View(cast);
        }

        // POST: Casts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CastID,RolePlayed,ActorID,MovieID")] Cast cast)
        {
            if (id != cast.CastID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CastExists(cast.CastID))
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
            ViewData["ActorID"] = new SelectList(_context.Actors, "ID", "Discriminator", cast.ActorID);
            ViewData["MovieID"] = new SelectList(_context.Movies, "MovieID", "MovieID", cast.MovieID);
            return View(cast);
        }
        [Authorize(Roles = "Administrator")]
        // GET: Casts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cast == null)
            {
                return NotFound();
            }

            var cast = await _context.Cast
                .Include(c => c.Actor)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.CastID == id);
            if (cast == null)
            {
                return NotFound();
            }

            return View(cast);
        }

        // POST: Casts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cast == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cast'  is null.");
            }
            var cast = await _context.Cast.FindAsync(id);
            if (cast != null)
            {
                _context.Cast.Remove(cast);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CastExists(int id)
        {
          return _context.Cast.Any(e => e.CastID == id);
        }
    }
}
