using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment1.Data;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class ClimbsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClimbsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Climbs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Climbs.Include(c => c.Gym);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Climbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Climbs == null)
            {
                return NotFound();
            }

            var climb = await _context.Climbs
                .Include(c => c.Gym)
                .FirstOrDefaultAsync(m => m.ClimbId == id);
            if (climb == null)
            {
                return NotFound();
            }

            return View(climb);
        }

        // GET: Climbs/Create
        public IActionResult Create()
        {
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "Name");
            return View();
        }

        // POST: Climbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClimbId,Colour,Style,Grade,StartDate,CompletionDate,GymId")] Climb climb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(climb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GymId"] = new SelectList(_context.Gyms.OrderBy(c=>c.Name), "GymId", "Name", climb.GymId);
            return View(climb);
        }

        // GET: Climbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Climbs == null)
            {
                return NotFound();
            }

            var climb = await _context.Climbs.FindAsync(id);
            if (climb == null)
            {
                return NotFound();
            }
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "Name", climb.GymId);
            return View(climb);
        }

        // POST: Climbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClimbId,Colour,Style,Grade,StartDate,CompletionDate,GymId")] Climb climb)
        {
            if (id != climb.ClimbId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(climb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClimbExists(climb.ClimbId))
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
            ViewData["GymId"] = new SelectList(_context.Gyms, "GymId", "Name", climb.GymId);
            return View(climb);
        }

        // GET: Climbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Climbs == null)
            {
                return NotFound();
            }

            var climb = await _context.Climbs
                .Include(c => c.Gym)
                .FirstOrDefaultAsync(m => m.ClimbId == id);
            if (climb == null)
            {
                return NotFound();
            }

            return View(climb);
        }

        // POST: Climbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Climbs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Climbs'  is null.");
            }
            var climb = await _context.Climbs.FindAsync(id);
            if (climb != null)
            {
                _context.Climbs.Remove(climb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClimbExists(int id)
        {
          return (_context.Climbs?.Any(e => e.ClimbId == id)).GetValueOrDefault();
        }
    }
}
