using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using carrent.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Carrent.Controllers
{
    [Authorize]
    public class RezervationsController : Controller
    {
        private readonly CarDbContext _context;
        private readonly UserManager<Clieunt> _userManager;

        public RezervationsController(CarDbContext context, UserManager<Clieunt> userManager)
        {
            _userManager= userManager;
            _context = context;
        }

        // GET: Rezervations

        
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("User"))
            {
                var carDbContext = _context.Rezervation.Include(r => r.Cars).Include(r => r.Clieunts).Where(x=>x.ClieuntId==_userManager.GetUserId(User));
                return View(await carDbContext.ToListAsync());
            }
            else
            {
                var carDbContext = _context.Rezervation.Include(r => r.Cars).Include(r => r.Clieunts);
                return View(await carDbContext.ToListAsync());
            }
            
        }

        // GET: Rezervations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rezervation == null)
            {
                return NotFound();
            }

            var rezervation = await _context.Rezervation
                .Include(r => r.Cars)
                .Include(r => r.Clieunts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezervation == null)
            {
                return NotFound();
            }

            return View(rezervation);
        }

        // GET: Rezervations/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Car, "Id", "Description");
            //ViewData["ClieuntId"] = new SelectList(_context.Users, "Id", "Description");

            return View();
        }

        // POST: Rezervations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,DateOn,DateOff")] Rezervation rezervation)
        {
            if (ModelState.IsValid)
            {
                rezervation.ClieuntId =  _userManager.GetUserId(User);
                rezervation.RegesterOn = DateTime.Now;
                _context.Rezervation.Add(rezervation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Car, "Id", "Description", rezervation.CarId);
            //ViewData["ClieuntId"] = new SelectList(_context.Users, "Id", "Id", rezervation.ClieuntId);
            return View(rezervation);
        }

        // GET: Rezervations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rezervation == null)
            {
                return NotFound();
            }

            var rezervation = await _context.Rezervation.FindAsync(id);
            if (rezervation == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Car, "Id", "Description", rezervation.CarId);
            //ViewData["ClieuntId"] = new SelectList(_context.Users, "Id", "Id", rezervation.ClieuntId);
            return View(rezervation);
        }

        // POST: Rezervations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,DateOn,DateOff")] Rezervation rezervation)
        {
            if (id != rezervation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    rezervation.ClieuntId = _userManager.GetUserId(User);
                    rezervation.RegesterOn = DateTime.Now;
                    _context.Rezervation.Update(rezervation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervationExists(rezervation.Id))
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
            ViewData["CarId"] = new SelectList(_context.Car, "Id", "Description", rezervation.CarId);
            //ViewData["ClieuntId"] = new SelectList(_context.Users, "Id", "Id", rezervation.ClieuntId);
            return View(rezervation);
        }

        // GET: Rezervations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rezervation == null)
            {
                return NotFound();
            }

            var rezervation = await _context.Rezervation
                .Include(r => r.Cars)
                .Include(r => r.Clieunts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezervation == null)
            {
                return NotFound();
            }

            return View(rezervation);
        }

        // POST: Rezervations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rezervation == null)
            {
                return Problem("Entity set 'CarDbContext.Rezervation'  is null.");
            }
            var rezervation = await _context.Rezervation.FindAsync(id);
            if (rezervation != null)
            {
                _context.Rezervation.Remove(rezervation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervationExists(int id)
        {
          return (_context.Rezervation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
