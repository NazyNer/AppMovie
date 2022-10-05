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
    public class ReturnController : Controller
    {
        private readonly AppMovieContext _context;

        public ReturnController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Return
        public async Task<IActionResult> Index()
        {
            var appMovieContext = _context.Return.Include(r => r.Partner);
            return View(await appMovieContext.ToListAsync());
        }

        // GET: Return/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Return == null)
            {
                return NotFound();
            }

            var @return = await _context.Return
                .Include(r => r.Partner)
                .FirstOrDefaultAsync(m => m.ReturnID == id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // GET: Return/Create
        public IActionResult Create()
        {
            ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName");
            ViewData["MovieID"] = new SelectList(_context.Movie.Where(x => x.estaAlquilada == true), "MovieID", "MovieName");
            return View();
        }

        // POST: Return/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReturnID,ReturnDate,PartnerID")] Return Return)
        {
            if (ModelState.IsValid)
            {
                using (var transaccion = _context.Database.BeginTransaction())
                {
                    try
                    {

                    _context.Add(Return);
                    await _context.SaveChangesAsync();
                    
                    var moviesTemp = (from a in _context.ReturnDetailTemp select a).ToList();
                    foreach (var item in moviesTemp)
                    {
                        var details = new ReturnDetail
                        {
                            ReturnID = Return.ReturnID,
                            MovieID = item.MovieID,
                            MovieName = item.MovieName
                        };
                        _context.ReturnDetail.Add(details);
                        _context.SaveChanges();
                    }
                    _context.ReturnDetailTemp.RemoveRange(moviesTemp);
                    _context.SaveChanges();
                    transaccion.Commit();
                    
                    return RedirectToAction(nameof(Index));
                    }
                    catch (System.Exception ex)
                    {
                        transaccion.Rollback();
                        var error = ex;
                    }
                }
            }
            
            ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName", Return.PartnerID);
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieName");
            ViewData["MovieID"] = new SelectList(_context.Movie.Where(x => x.estaAlquilada == true), "MovieID", "MovieName");
            return View(Return);
        }

        // GET: Return/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Return == null)
            {
                return NotFound();
            }

            var @return = await _context.Return.FindAsync(id);
            if (@return == null)
            {
                return NotFound();
            }
            ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName", @return.PartnerID);
            return View(@return);
        }

        // POST: Return/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReturnID,ReturnDate,PartnerID")] Return @return)
        {
            if (id != @return.ReturnID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@return);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReturnExists(@return.ReturnID))
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
            ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName", @return.PartnerID);
            return View(@return);
        }

        // GET: Return/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Return == null)
            {
                return NotFound();
            }

            var @return = await _context.Return
                .Include(r => r.Partner)
                .FirstOrDefaultAsync(m => m.ReturnID == id);
            if (@return == null)
            {
                return NotFound();
            }

            return View(@return);
        }

        // POST: Return/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Return == null)
            {
                return Problem("Entity set 'AppMovieContext.Return'  is null.");
            }
            var @return = await _context.Return.FindAsync(id);
            if (@return != null)
            {
                _context.Return.Remove(@return);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public JsonResult SearchMovieTmp()
        {
            List<ReturnDetailTemp> ListadoMovieTmp = new List<ReturnDetailTemp>();
            var returnDetailTemp = (from a in _context.ReturnDetailTemp select a).ToList();
            foreach (var item in returnDetailTemp)
            {
                ListadoMovieTmp.Add(item);
            }
            return Json(ListadoMovieTmp);
        }
        
        public JsonResult AgregarPeliculas(int MovieID)
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var movie = (from a in _context.Movie where a.MovieID == MovieID select a).SingleOrDefault();
                    movie.estaAlquilada = false;
                    _context.SaveChanges();

                    var movieTemp = new ReturnDetailTemp
                    {
                        MovieID = movie.MovieID,
                        MovieName = movie.MovieName
                    };
                    _context.ReturnDetailTemp.Add(movieTemp);
                    _context.SaveChanges();
                    transaccion.Commit();
                }
                catch (System.Exception)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }
            ViewData["MovieID"] = new SelectList(_context.Movie.Where(x => x.estaAlquilada == true), "MovieID", "MovieName");
            return Json(resultado);
        }

        public JsonResult CancelarDevolucion()
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var returnTemp = (from a in _context.ReturnDetailTemp select a).ToList();

                    foreach (var item in returnTemp)
                    {
                        var movie = (from a in _context.Movie where a.MovieID == item.MovieID select a).SingleOrDefault();
                        movie.estaAlquilada = true;
                        _context.SaveChanges();
                    }

                    _context.ReturnDetailTemp.RemoveRange(returnTemp);
                    _context.SaveChanges();
                    transaccion.Commit();

                }
                catch (System.Exception)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }
            return Json(resultado);
        }

        public JsonResult QuitarMovie(int MovieID)
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try{
                    var movie = (from a in _context.Movie where a.MovieID == MovieID select a).SingleOrDefault();
                    movie.estaAlquilada = true;
                    _context.SaveChanges();

                    var returnTemp = (from a in _context.ReturnDetailTemp where a.MovieID == MovieID select a).SingleOrDefault();
                    _context.ReturnDetailTemp.Remove(returnTemp);
                    _context.SaveChanges();

                    transaccion.Commit();
                }
                catch (System.Exception){
                    transaccion.Rollback();
                    resultado = false;
                }
            }   

            return Json(resultado);      
        }

        public JsonResult SearchMovie(int ReturnID)
        {
            List<ReturnDetail> ListadoMovie = new List<ReturnDetail>();
            var returnDetail = (from a in _context.ReturnDetail where a.ReturnID == ReturnID select a).ToList();
            foreach (var item in returnDetail)
            {
                ListadoMovie.Add(item);
            }
            return Json(ListadoMovie);
        }

        private bool ReturnExists(int id)
        {
            return (_context.Return?.Any(e => e.ReturnID == id)).GetValueOrDefault();
        }
    }
}
