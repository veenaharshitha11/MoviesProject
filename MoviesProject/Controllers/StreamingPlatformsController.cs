//Veena Harshitha Gandhe
using System;
using System.Collections.Generic;
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
    public class StreamingPlatformsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StreamingPlatformsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StreamingPlatforms
        public async Task<IActionResult> Index()
        {
              return View(await _context.StreamingPlatforms.ToListAsync());
        }

        // GET: StreamingPlatforms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StreamingPlatforms == null)
            {
                return NotFound();
            }

            var streamingPlatform = await _context.StreamingPlatforms
                .FirstOrDefaultAsync(m => m.StreamingPlatformID == id);
            if (streamingPlatform == null)
            {
                return NotFound();
            }

            return View(streamingPlatform);
        }
        [Authorize(Roles = "Administrator")]
        // GET: StreamingPlatforms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StreamingPlatforms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StreamingPlatformID,PlatformName,SubscriptionCost")] StreamingPlatform streamingPlatform)
        {
            if (ModelState.IsValid)
            {
                _context.Add(streamingPlatform);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(streamingPlatform);
        }
        [Authorize(Roles = "Administrator")]
        // GET: StreamingPlatforms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StreamingPlatforms == null)
            {
                return NotFound();
            }

            var streamingPlatform = await _context.StreamingPlatforms.FindAsync(id);
            if (streamingPlatform == null)
            {
                return NotFound();
            }
            return View(streamingPlatform);
        }

        // POST: StreamingPlatforms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StreamingPlatformID,PlatformName,SubscriptionCost")] StreamingPlatform streamingPlatform)
        {
            if (id != streamingPlatform.StreamingPlatformID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(streamingPlatform);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreamingPlatformExists(streamingPlatform.StreamingPlatformID))
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
            return View(streamingPlatform);
        }
        [Authorize(Roles = "Administrator")]
        // GET: StreamingPlatforms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StreamingPlatforms == null)
            {
                return NotFound();
            }

            var streamingPlatform = await _context.StreamingPlatforms
                .FirstOrDefaultAsync(m => m.StreamingPlatformID == id);
            if (streamingPlatform == null)
            {
                return NotFound();
            }

            return View(streamingPlatform);
        }

        // POST: StreamingPlatforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StreamingPlatforms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StreamingPlatforms'  is null.");
            }
            var streamingPlatform = await _context.StreamingPlatforms.FindAsync(id);
            if (streamingPlatform != null)
            {
                _context.StreamingPlatforms.Remove(streamingPlatform);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StreamingPlatformExists(int id)
        {
          return _context.StreamingPlatforms.Any(e => e.StreamingPlatformID == id);
        }
    }
}
