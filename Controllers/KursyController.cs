using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JRG.WebApplication.Data;

namespace JRG.WebApplication.Controllers
{
    public class KursyController : Controller
    {
        private readonly PracownikContext _context;

        public KursyController(PracownikContext context)
        {
            _context = context;
        }

        // GET: Kursy
        public async Task<IActionResult> Index()
        {
              return _context.Kursy != null ? 
                          View(await _context.Kursy.ToListAsync()) :
                          Problem("Entity set 'PracownikContext.Kursy'  is null.");
        }

        // GET: Kursy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kursy == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kursy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kurs == null)
            {
                return NotFound();
            }

            return View(kurs);
        }

        // GET: Kursy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kursy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa")] Kurs kurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kurs);
        }

        // GET: Kursy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kursy == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kursy.FindAsync(id);
            if (kurs == null)
            {
                return NotFound();
            }
            return View(kurs);
        }

        // POST: Kursy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa")] Kurs kurs)
        {
            if (id != kurs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KursExists(kurs.Id))
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
            return View(kurs);
        }

        // GET: Kursy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kursy == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kursy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kurs == null)
            {
                return NotFound();
            }

            return View(kurs);
        }

        // POST: Kursy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kursy == null)
            {
                return Problem("Entity set 'PracownikContext.Kursy'  is null.");
            }
            var kurs = await _context.Kursy.FindAsync(id);
            if (kurs != null)
            {
                _context.Kursy.Remove(kurs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KursExists(int id)
        {
          return (_context.Kursy?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
