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
    public class ProducerController : Controller
    {
        private readonly AppMovieContext _context;

        public ProducerController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Producer
        public async Task<IActionResult> Index()
        {
            return _context.Producer != null ? 
                        View(await _context.Producer.ToListAsync()) :
                        Problem("Entity set 'AppMovieContext.Producer'  is null.");
        }

        // GET: Producer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Producer == null)
            {
                return NotFound();
            }

            var producer = await _context.Producer
                .FirstOrDefaultAsync(m => m.ProducerId == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // GET: Producer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProducerId,ProducerName")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Producer == null)
            {
                return NotFound();
            }

            var producer = await _context.Producer.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProducerId,ProducerName")] Producer producer)
        {
            if (id != producer.ProducerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerExists(producer.ProducerId))
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
            return View(producer);
        }

        // GET: Producer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Producer == null)
            {
                return NotFound();
            }

            var producer = await _context.Producer
                .FirstOrDefaultAsync(m => m.ProducerId == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producer/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = await _context.Producer.FindAsync(id);
            if (producer != null)
            {
                var producerInMovie = (from a in  _context.Movie where a.ProducerID == id select a).ToList();
                if (producerInMovie.Count == 0)
                {
                _context.Producer.Remove(producer);
                await _context.SaveChangesAsync();
                }
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
            return (_context.Producer?.Any(e => e.ProducerId == id)).GetValueOrDefault();
        }
    }
}
