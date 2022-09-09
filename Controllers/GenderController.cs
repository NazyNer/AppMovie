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
    public class GenderController : Controller
    {
        private readonly AppMovieContext _context;

        public GenderController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Gender
        public async Task<IActionResult> Index()
        {
              return _context.Gender != null ? 
                          View(await _context.Gender.ToListAsync()) :
                          Problem("Entity set 'AppMovieContext.Gender'  is null.");
        }

        // GET: Gender/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gender == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender
                .FirstOrDefaultAsync(m => m.GenderId == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // GET: Gender/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gender/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenderId,GenderName")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }

        // GET: Gender/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gender == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        // POST: Gender/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GenderId,GenderName")] Gender gender)
        {
            if (id != gender.GenderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenderExists(gender.GenderId))
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
            return View(gender);
        }

        // GET: Gender/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gender == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender
                .FirstOrDefaultAsync(m => m.GenderId == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // POST: Gender/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gender = await _context.Gender.FindAsync(id);
            if (gender != null)
            {
                var genderInMovie = (from a in  _context.Movie where a.GenderID == id select a).ToList();
                if (genderInMovie.Count == 0)
                {
                _context.Gender.Remove(gender);
                await _context.SaveChangesAsync();
                }

            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool GenderExists(int id)
        {
          return (_context.Gender?.Any(e => e.GenderId == id)).GetValueOrDefault();
        }
    }
}
