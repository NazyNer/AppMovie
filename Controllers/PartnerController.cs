using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMovie.Models;

namespace AppMovie.Controllers
{
    public class PartnerController : Controller
    {
        private readonly AppMovieContext _context;

        public PartnerController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Partner
        public async Task<IActionResult> Index()
        {
            var appMovieContext = _context.Partner.Include(p => p.Location);
            return View(await appMovieContext.ToListAsync());
        }

        // GET: Partner/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Partner == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .Include(p => p.Location)
                .FirstOrDefaultAsync(m => m.PartnerID == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // GET: Partner/Create
        public IActionResult Create()
        {
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationId", "LocationName");
            return View();
        }

        // POST: Partner/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartnerID,PartnerName,PartnerDirection,PartnerPhone,PartnerBirthDate,LocationID")] Partner partner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationId", "LocationName", partner.LocationID);
            return View(partner);
        }

        // GET: Partner/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Partner == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner.FindAsync(id);
            if (partner == null)
            {
                return NotFound();
            }
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationId", "LocationName", partner.LocationID);
            return View(partner);
        }

        // POST: Partner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartnerID,PartnerName,PartnerDirection,PartnerPhone,PartnerBirthDate,LocationID")] Partner partner)
        {
            if (id != partner.PartnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnerExists(partner.PartnerID))
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
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationId", "LocationName", partner.LocationID);
            return View(partner);
        }

        // GET: Partner/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Partner == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .Include(p => p.Location)
                .FirstOrDefaultAsync(m => m.PartnerID == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Partner == null)
            {
                return Problem("Entity set 'AppMovieContext.Partner'  is null.");
            }
            var partner = await _context.Partner.FindAsync(id);
            if (partner != null)
            {
                _context.Partner.Remove(partner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartnerExists(int id)
        {
          return (_context.Partner?.Any(e => e.PartnerID == id)).GetValueOrDefault();
        }
    }
}
