//Veena Harshitha Gandhe
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
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movies.Include(m => m.Director).Include(m => m.StreamingPlatform);
            var sessionClicks = HttpContext.Session.Get<List<Movie>>("UserMovieClicks");
            if (sessionClicks != null)
            {
                ViewBag.UserMovieClicks = sessionClicks;
            }
            return View(await applicationDbContext.ToListAsync());
        }
        private void AddClickedMovieToSession(Movie movie)
        {
            var sessionClicks = HttpContext.Session.Get<List<Movie>>("UserMovieClicks");
            if (sessionClicks == null)
            {
                sessionClicks = new List<Movie>();
            }
            var movieInSession = sessionClicks.FirstOrDefault(m => m.MovieID == movie.MovieID);
            if (movieInSession == null)
            {
                sessionClicks.Add(movie);
                HttpContext.Session.Set("UserMovieClicks", sessionClicks);
            }
        }
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Director)
                .Include(m => m.StreamingPlatform)
                .Include(m => m.Cast).ThenInclude(e => e.Actor)
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }
            AddClickedMovieToSession(movie);
            return View(movie);
        }
        [Authorize(Roles = "Administrator")]
        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["DirectorID"] = new SelectList(_context.Directors, "ID", "LastName");
            ViewData["StreamingPlatformID"] = new SelectList(_context.StreamingPlatforms, "StreamingPlatformID", "PlatformName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieID,Title,ReleaseDate,Genre,DirectorID,StreamingPlatformID")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorID"] = new SelectList(_context.Directors, "ID", "Discriminator", movie.DirectorID);
            ViewData["StreamingPlatformID"] = new SelectList(_context.StreamingPlatforms, "StreamingPlatformID", "StreamingPlatformID", movie.StreamingPlatformID);
            return View(movie);
        }
        [Authorize(Roles = "Administrator")]
        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["DirectorID"] = new SelectList(_context.Directors, "ID", "LastName", movie.DirectorID);
            ViewData["StreamingPlatformID"] = new SelectList(_context.StreamingPlatforms, "StreamingPlatformID", "PlatformName", movie.StreamingPlatformID);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieID,Title,ReleaseDate,Genre,DirectorID,StreamingPlatformID")] Movie movie)
        {
            if (id != movie.MovieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieID))
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
            ViewData["DirectorID"] = new SelectList(_context.Directors, "ID", "Discriminator", movie.DirectorID);
            ViewData["StreamingPlatformID"] = new SelectList(_context.StreamingPlatforms, "StreamingPlatformID", "StreamingPlatformID", movie.StreamingPlatformID);
            return View(movie);
        }
        [Authorize(Roles = "Administrator")]
        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Director)
                .Include(m => m.StreamingPlatform)
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return _context.Movies.Any(e => e.MovieID == id);
        }
    }
}
