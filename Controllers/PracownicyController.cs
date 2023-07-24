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
    public class PracownicyController : Controller
    {
        private readonly PracownikContext _context;

        public PracownicyController(PracownikContext context)
        {
            _context = context;
        }

        // GET: Pracownicy
        public async Task<IActionResult> Index()
        {
            var pracownikContext = _context.Pracownicy.Include(p => p.Stopien).Include(p => p.Zmiana).Include(p => p.UkonczoneKursy);
            return View(await pracownikContext.ToListAsync());
        }

        // GET: Pracownicy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pracownicy == null)
            {
                return NotFound();
            }

            var pracownik = await _context.Pracownicy
                .Include(p => p.Stopien)
                .Include(p => p.Zmiana)
                .Include(p => p.UkonczoneKursy)
                .ThenInclude(p => p.Kurs)
                .Include(p=> p.Uprawnienia)
                .ThenInclude(p => p.prawko)
                .FirstOrDefaultAsync(m => m.Id == id)
                ;

            if (pracownik == null)
            {
                return NotFound();
            }

            return View(pracownik);
        }

        // GET: Pracownicy/Create
        public IActionResult Create()
        {
            ViewData["StopienId"] = new SelectList(_context.Stopnie, "Id", "Name");
            ViewData["ZmianaId"] = new SelectList(_context.Zmiany, "Id", "Name");
            ViewData["MultiPrawko"] = new SelectList(_context.Prawko, "Id", "Nazwa");
            ViewData["MultiKursy"] = new SelectList(_context.Kursy, "Id", "Nazwa");

            return View();
        }

        // POST: Pracownicy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int[] PrawkoId, int[] KursId, [Bind("Id,Nazwa,Notatka,DataUrodzenia,DataZatrudnienia,Telefon,Adres,StopienId,ZmianaId")] Pracownik pracownik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pracownik);
                await _context.SaveChangesAsync();
                //pasted
                foreach (int prawkoid in PrawkoId)
                {
                    Uprawnienie uprawnienie = new Uprawnienie {PracownikId = pracownik.Id, PrawkoId = prawkoid};
                    uprawnienie.PrawkoId = prawkoid;
                    uprawnienie.PracownikId = pracownik.Id;
                    var uprawnieniaUzytkownika = _context.Uprawnienia.FirstOrDefault(x =>
                        x.PracownikId == uprawnienie.PracownikId && x.PrawkoId == uprawnienie.PrawkoId);
                    if (uprawnieniaUzytkownika == null)
                    {
                        _context.Add(uprawnienie);
                        await _context.SaveChangesAsync();
                    }
                }

                foreach (int kursid in KursId)
                {
	                UkonczonyKurs ukonczonyKurs = new UkonczonyKurs { PracownikId = pracownik.Id, KursId = kursid };
	                ukonczonyKurs.KursId = kursid;
	                ukonczonyKurs.PracownikId = pracownik.Id;
	                var ukonczoneKursy = _context.UkonczoneKursy.FirstOrDefault(x =>
		                x.PracownikId == ukonczonyKurs.PracownikId && x.KursId == ukonczonyKurs.KursId);
	                if (ukonczoneKursy == null)
	                {
		                _context.Add(ukonczonyKurs);
		                await _context.SaveChangesAsync();
	                }
                }

				//pasted
				return RedirectToAction(nameof(Index));
			}
			ViewData["StopienId"] = new SelectList(_context.Stopnie, "Id", "Name", pracownik.StopienId);
			ViewData["ZmianaId"] = new SelectList(_context.Zmiany, "Id", "Name", pracownik.ZmianaId);
			ViewData["MultiPrawko"] = new SelectList(_context.Prawko, "Id", "Nazwa", pracownik.Uprawnienia);
			ViewData["MultiKursy"] = new SelectList(_context.Kursy, "Id", "Nazwa", pracownik.UkonczoneKursy);

			return View(pracownik);
        }

        // GET: Pracownicy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pracownicy == null)
            {
                return NotFound();
            }

			var pracownik = await _context.Pracownicy
					.Include(p => p.Stopien)
					.Include(p => p.Zmiana)
					.Include(p => p.UkonczoneKursy)
					.ThenInclude(p => p.Kurs)
					.Include(p => p.Uprawnienia)
					.ThenInclude(p => p.prawko)
					.FirstOrDefaultAsync(m => m.Id == id)
				;
			if (pracownik == null)
            {
                return NotFound();
			}
            ViewData["StopienId"] = new SelectList(_context.Stopnie, "Id", "Name", pracownik.StopienId);
            ViewData["ZmianaId"] = new SelectList(_context.Zmiany, "Id", "Name", pracownik.ZmianaId);
            ViewData["MultiPrawko"] = new MultiSelectList(_context.Prawko, "Id", "Nazwa", pracownik.Uprawnienia.Select(c => c.PrawkoId).ToArray());
			ViewData["MultiKursy"] = new MultiSelectList(_context.Kursy, "Id", "Nazwa", pracownik.UkonczoneKursy.Select(c => c.KursId).ToArray());
			return View(pracownik);
        }

        // POST: Pracownicy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int[] PrawkoId, int[] KursId, [Bind("Id,Nazwa,Notatka,DataUrodzenia,DataZatrudnienia,Telefon,Adres,StopienId,ZmianaId")] Pracownik pracownik)
        {
            if (id != pracownik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pracownik);
                    await _context.SaveChangesAsync();

                    var pracownik2 = await _context.Pracownicy
		                    .Include(p => p.Stopien)
		                    .Include(p => p.Zmiana)
		                    .Include(p => p.UkonczoneKursy)
		                    .ThenInclude(p => p.Kurs)
		                    .Include(p => p.Uprawnienia)
		                    .ThenInclude(p => p.prawko)
		                    .FirstOrDefaultAsync(m => m.Id == id)
	                    ;

					 _context.Uprawnienia.RemoveRange(pracownik2.Uprawnienia);
					 _context.SaveChanges();

					foreach (int prawkoid in PrawkoId)
                    {
	                    Uprawnienie uprawnienie = new Uprawnienie { PracownikId = pracownik.Id, PrawkoId = prawkoid };
	                    uprawnienie.PrawkoId = prawkoid;
	                    uprawnienie.PracownikId = pracownik.Id;
	                    var uprawnieniaUzytkownika = _context.Uprawnienia.FirstOrDefault(x =>
		                    x.PracownikId == uprawnienie.PracownikId && x.PrawkoId == uprawnienie.PrawkoId);
	                    if (uprawnieniaUzytkownika == null)
	                    {
		                    _context.Add(uprawnienie);
		                    await _context.SaveChangesAsync();
	                    }
                    }

					_context.UkonczoneKursy.RemoveRange(pracownik2.UkonczoneKursy);
					_context.SaveChanges();


					foreach (int kursid in KursId)
                    {
	                    UkonczonyKurs ukonczonyKurs = new UkonczonyKurs { PracownikId = pracownik.Id, KursId = kursid };
	                    ukonczonyKurs.KursId = kursid;
	                    ukonczonyKurs.PracownikId = pracownik.Id;
	                    var ukonczoneKursy = _context.UkonczoneKursy.FirstOrDefault(x =>
		                    x.PracownikId == ukonczonyKurs.PracownikId && x.KursId == ukonczonyKurs.KursId);
	                    if (ukonczoneKursy == null)
	                    {
		                    _context.Add(ukonczonyKurs);
		                    await _context.SaveChangesAsync();
	                    }
                    }

				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!PracownikExists(pracownik.Id))
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
            ViewData["StopienId"] = new SelectList(_context.Stopnie, "Id", "Name", pracownik.StopienId);
            ViewData["ZmianaId"] = new SelectList(_context.Zmiany, "Id", "Name", pracownik.ZmianaId);
            ViewData["MultiPrawko"] = new SelectList(_context.Prawko, "Id", "Nazwa", pracownik.Uprawnienia);
            ViewData["MultiKursy"] = new SelectList(_context.Kursy, "Id", "Nazwa", pracownik.UkonczoneKursy);
            
			return View(pracownik);
        }

        // GET: Pracownicy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pracownicy == null)
            {
                return NotFound();
            }

            var pracownik = await _context.Pracownicy
                .Include(p => p.Stopien)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pracownik == null)
            {
                return NotFound();
            }

            return View(pracownik);
        }

        // POST: Pracownicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pracownicy == null)
            {
                return Problem("Entity set 'PracownikContext.Pracownicy'  is null.");
            }
            var pracownik = await _context.Pracownicy.FindAsync(id);
            if (pracownik != null)
            {
                _context.Pracownicy.Remove(pracownik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PracownikExists(int id)
        {
          return (_context.Pracownicy?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
