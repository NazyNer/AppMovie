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
    public class RentalController : Controller
    {
        private readonly AppMovieContext _context;

        public RentalController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Rental
        public async Task<IActionResult> Index()
        {
            var appMovieContext = _context.Rental.Include(r => r.Partner);
            return View(await appMovieContext.ToListAsync());
        }

        // GET: Rental/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.Partner)
                .FirstOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rental/Create
        public IActionResult Create()
        {
            ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName");
            ViewData["MovieID"] = new SelectList(_context.Movie.Where(x => x.estaAlquilada == false), "MovieID", "MovieName");
            return View();
        }

        // POST: Rental/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalID,RentalDate,PartnerID")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName", rental.PartnerID);
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieName");
            ViewData["MovieID"] = new SelectList(_context.Movie.Where(x => x.estaAlquilada == false), "MovieID", "MovieName");
            return View(rental);
        }

        // GET: Rental/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName", rental.PartnerID);
            return View(rental);
        }

        // POST: Rental/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalID,RentalDate,PartnerID")] Rental rental)
        {
            if (id != rental.RentalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.RentalID))
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
            ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName", rental.PartnerID);
            return View(rental);
        }

        // GET: Rental/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.Partner)
                .FirstOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rental == null)
            {
                return Problem("Entity set 'AppMovieContext.Rental'  is null.");
            }
            var rental = await _context.Rental.FindAsync(id);
            if (rental != null)
            {
                _context.Rental.Remove(rental);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public JsonResult AgregarPeliculas(int MovieID)
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var movie = (from a in _context.Movie where a.MovieID == MovieID select a).SingleOrDefault();
                    movie.estaAlquilada = true;
                    _context.SaveChanges();

                    var movieTemp = new RentalDetailTemp
                    {
                        MovieID = movie.MovieID,
                        MovieName = movie.MovieName
                    };
                    _context.RentalDetailTemp.Add(movieTemp);
                    _context.SaveChanges();
                    transaccion.Commit();
                }
                catch (System.Exception)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }
            ViewData["MovieID"] = new SelectList(_context.Movie.Where(x => x.estaAlquilada == false), "MovieID", "MovieName");
            return Json(resultado);
        }

        public JsonResult CancelarAlquiler()
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var rentalTemp = (from a in _context.RentalDetailTemp select a).ToList();

                    foreach (var item in rentalTemp)
                    {
                        var movie = (from a in _context.Movie where a.MovieID == item.MovieID select a).SingleOrDefault();
                        movie.estaAlquilada = false;
                        _context.SaveChanges();
                    }

                    _context.RentalDetailTemp.RemoveRange(rentalTemp);
                    _context.SaveChanges();

                    transaccion.Commit();

                }
                catch (System.Exception)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }
            ViewData["MovieID"] = new SelectList(_context.Movie.Where(x => x.estaAlquilada == false), "MovieID", "MovieName");
            return Json(resultado);
        }


        public JsonResult BuscarPeliculas()
        {
            List<RentalDetailTemp> listadoPeliculasDT = new List<RentalDetailTemp>();

            var peliculasDT = (from a in _context.RentalDetailTemp select a).ToList();

            foreach (var item in peliculasDT)
            {
                var guardarPelicula = new RentalDetailTemp
                {
                    MovieID = item.MovieID,
                    MovieName = item.MovieName
                };
                listadoPeliculasDT.Add(guardarPelicula);
            }

            return Json(listadoPeliculasDT);
        }

        private bool RentalExists(int id)
        {
          return (_context.Rental?.Any(e => e.RentalID == id)).GetValueOrDefault();
        }
    }
}
