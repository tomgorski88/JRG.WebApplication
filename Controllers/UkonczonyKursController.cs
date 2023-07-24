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
    public class UkonczonyKursController : Controller
    {
        private readonly PracownikContext _context;

        public UkonczonyKursController(PracownikContext context)
        {
            _context = context;
        }

        // GET: UkonczonyKurs
        public async Task<IActionResult> Index()
        {
            var pracownikContext = _context.UkonczoneKursy.Include(u => u.Kurs).Include(u => u.Pracownik);
            return View(await pracownikContext.ToListAsync());
        }

        // GET: UkonczonyKurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UkonczoneKursy == null)
            {
                return NotFound();
            }

            var ukonczonyKurs = await _context.UkonczoneKursy
                .Include(u => u.Kurs)
                .Include(u => u.Pracownik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ukonczonyKurs == null)
            {
                return NotFound();
            }

            return View(ukonczonyKurs);
        }

        // GET: UkonczonyKurs/Create
        public IActionResult Create()
        {
            ViewData["KursId"] = new SelectList(_context.Kursy, "Id", "Nazwa");
            ViewData["PracownikId"] = new SelectList(_context.Pracownicy, "Id", "Nazwa");
            return View();
        }

        // POST: UkonczonyKurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PracownikId,KursId")] UkonczonyKurs ukonczonyKurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ukonczonyKurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KursId"] = new SelectList(_context.Kursy, "Id", "Nazwa", ukonczonyKurs.KursId);
            ViewData["PracownikId"] = new SelectList(_context.Pracownicy, "Id", "Nazwa", ukonczonyKurs.PracownikId);
            return View(ukonczonyKurs);
        }

        // GET: UkonczonyKurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UkonczoneKursy == null)
            {
                return NotFound();
            }

            var ukonczonyKurs = await _context.UkonczoneKursy.FindAsync(id);
            if (ukonczonyKurs == null)
            {
                return NotFound();
            }
            ViewData["KursId"] = new SelectList(_context.Kursy, "Id", "Nazwa", ukonczonyKurs.KursId);
            ViewData["PracownikId"] = new SelectList(_context.Pracownicy, "Id", "Nazwa", ukonczonyKurs.PracownikId);
            return View(ukonczonyKurs);
        }

        // POST: UkonczonyKurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PracownikId,KursId")] UkonczonyKurs ukonczonyKurs)
        {
            if (id != ukonczonyKurs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ukonczonyKurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UkonczonyKursExists(ukonczonyKurs.Id))
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
            ViewData["KursId"] = new SelectList(_context.Kursy, "Id", "Nazwa", ukonczonyKurs.KursId);
            ViewData["PracownikId"] = new SelectList(_context.Pracownicy, "Id", "Nazwa", ukonczonyKurs.PracownikId);
            return View(ukonczonyKurs);
        }

        // GET: UkonczonyKurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UkonczoneKursy == null)
            {
                return NotFound();
            }

            var ukonczonyKurs = await _context.UkonczoneKursy
                .Include(u => u.Kurs)
                .Include(u => u.Pracownik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ukonczonyKurs == null)
            {
                return NotFound();
            }

            return View(ukonczonyKurs);
        }

        // POST: UkonczonyKurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UkonczoneKursy == null)
            {
                return Problem("Entity set 'PracownikContext.UkonczoneKursy'  is null.");
            }
            var ukonczonyKurs = await _context.UkonczoneKursy.FindAsync(id);
            if (ukonczonyKurs != null)
            {
                _context.UkonczoneKursy.Remove(ukonczonyKurs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UkonczonyKursExists(int id)
        {
          return (_context.UkonczoneKursy?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
