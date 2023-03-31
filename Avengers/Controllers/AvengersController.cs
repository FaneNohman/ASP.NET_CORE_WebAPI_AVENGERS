using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Avengers.Models;
using CORE_MVC_EXAM.Models;

namespace Avengers.Controllers
{
    public class AvengersController : Controller
    {
        private readonly AppDbContext _context;

        public AvengersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Avengers
        public async Task<IActionResult> Index()
        {
              return _context.Avengers != null ? 
                          View(await _context.Avengers.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Avengers'  is null.");
        }

        // GET: Avengers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Avengers == null)
            {
                return NotFound();
            }

            var avenger = await _context.Avengers
                .FirstOrDefaultAsync(m => m.AvengerId == id);
            if (avenger == null)
            {
                return NotFound();
            }

            return View(avenger);
        }

        // GET: Avengers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Avengers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvengerId,RealName,HeroName")] Avenger avenger)
        {
            if (ModelState.IsValid)
            {
                avenger.AvengerId = Guid.NewGuid();
                _context.Add(avenger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avenger);
        }

        // GET: Avengers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Avengers == null)
            {
                return NotFound();
            }

            var avenger = await _context.Avengers.FindAsync(id);
            if (avenger == null)
            {
                return NotFound();
            }
            return View(avenger);
        }

        // POST: Avengers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AvengerId,RealName,HeroName")] Avenger avenger)
        {
            if (id != avenger.AvengerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avenger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvengerExists(avenger.AvengerId))
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
            return View(avenger);
        }

        // GET: Avengers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Avengers == null)
            {
                return NotFound();
            }

            var avenger = await _context.Avengers
                .FirstOrDefaultAsync(m => m.AvengerId == id);
            if (avenger == null)
            {
                return NotFound();
            }

            return View(avenger);
        }

        // POST: Avengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Avengers == null)
            {
                return Problem("Entity set 'AppDbContext.Avengers'  is null.");
            }
            var avenger = await _context.Avengers.FindAsync(id);
            if (avenger != null)
            {
                _context.Avengers.Remove(avenger);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvengerExists(Guid id)
        {
          return (_context.Avengers?.Any(e => e.AvengerId == id)).GetValueOrDefault();
        }
    }
}
