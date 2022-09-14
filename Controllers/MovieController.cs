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
    public class MovieController : Controller
    {
        private readonly AppMovieContext _context;

        public MovieController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var appMovieContext = _context.Movie.Include(m => m.Gender).Include(m => m.Producer).Include(m => m.Section);
            return View(await appMovieContext.ToListAsync());
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Gender)
                .Include(m => m.Producer)
                .Include(m => m.Section)
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            ViewData["GenderID"] = new SelectList(_context.Gender, "GenderId", "GenderName");
            ViewData["ProducerID"] = new SelectList(_context.Producer, "ProducerId", "ProducerName");
            ViewData["SectionID"] = new SelectList(_context.Section, "SectionId", "SectionName");
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieID,MovieName,MovieDescription,MovieDate,SectionID,GenderID,ProducerID, estaAlquilada")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderID"] = new SelectList(_context.Gender, "GenderId", "GenderName", movie.GenderID);
            ViewData["ProducerID"] = new SelectList(_context.Producer, "ProducerId", "ProducerName", movie.ProducerID);
            ViewData["SectionID"] = new SelectList(_context.Section, "SectionId", "SectionName", movie.SectionID);
            return View(movie);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["GenderID"] = new SelectList(_context.Gender, "GenderId", "GenderName", movie.GenderID);
            ViewData["ProducerID"] = new SelectList(_context.Producer, "ProducerId", "ProducerName", movie.ProducerID);
            ViewData["SectionID"] = new SelectList(_context.Section, "SectionId", "SectionName", movie.SectionID);
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieID,MovieName,MovieDescription,MovieDate,SectionID,GenderID,ProducerID, estaAlquilada")] Movie movie)
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
            ViewData["GenderID"] = new SelectList(_context.Gender, "GenderId", "GenderName", movie.GenderID);
            ViewData["ProducerID"] = new SelectList(_context.Producer, "ProducerId", "ProducerName", movie.ProducerID);
            ViewData["SectionID"] = new SelectList(_context.Section, "SectionId", "SectionName", movie.SectionID);
            return View(movie);
        }

        // GET: Movie/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null || _context.Movie == null)
        //     {
        //         return NotFound();
        //     }

        //     var movie = await _context.Movie
        //         .Include(m => m.Gender)
        //         .Include(m => m.Producer)
        //         .Include(m => m.Section)
        //         .FirstOrDefaultAsync(m => m.MovieID == id);
        //     if (movie == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(movie);
        // }

        // POST: Movie/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                var movieAlquilada = (from a in _context.RentalDetail where a.MovieID == id select a).Count();
                if(movieAlquilada == 0)
                {
                    _context.Movie.Remove(movie);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    movie.IsDeleted = true;
                    movie.MovieName = "Eliminada";
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.MovieID == id)).GetValueOrDefault();
        }
    }
}
