using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using carrent.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Carrent.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarDbContext _context;

        public CarsController(CarDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var carDbContext = _context.Car.Include(c => c.BrandModels);
            return View(await carDbContext.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.BrandModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["BrandModelId"] = _context.BrandModel.Select(x => new SelectListItem
            {
                Text = x.Brand + " " + x.Model,
                Value = x.Id.ToString()
            }
            );
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandModelId,Year,Description,ImageUrl,Status,Price")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.RegesterOn=DateTime.Now;
                _context.Car.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandModelId"] = _context.BrandModel.Select(x => new SelectListItem
            {
                Text = x.Brand + " " + x.Model,
                Value = x.Id.ToString()
            }
             );
            return View(car);
        }
        [Authorize(Roles = "Admin")]
        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["BrandModelId"] = _context.BrandModel.Select(x => new SelectListItem { Text = x.Brand + " " + x.Model, Value = x.Id.ToString() });
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrandModelId,Year,Description,ImageUrl,Status,Price")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    car.RegesterOn=DateTime.Now;
                    _context.Car.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewData["BrandModelId"] = _context.BrandModel.Select(x => new SelectListItem { Text = x.Brand + " " + x.Model, Value = x.Id.ToString() });
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.BrandModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Car == null)
            {
                return Problem("Entity set 'CarDbContext.Car'  is null.");
            }
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
          return (_context.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
