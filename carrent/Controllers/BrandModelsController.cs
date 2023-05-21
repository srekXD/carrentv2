using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using carrent.Data;

namespace Carrent.Controllers
{
    public class BrandModelsController : Controller
    {
        private readonly CarDbContext _context;

        public BrandModelsController(CarDbContext context)
        {
            _context = context;
        }

        // GET: BrandModels
        public async Task<IActionResult> Index()
        {
              return _context.BrandModel != null ? 
                          View(await _context.BrandModel.ToListAsync()) :
                          Problem("Entity set 'CarDbContext.BrandModel'  is null.");
        }

        // GET: BrandModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BrandModel == null)
            {
                return NotFound();
            }

            var brandModel = await _context.BrandModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brandModel == null)
            {
                return NotFound();
            }

            return View(brandModel);
        }

        // GET: BrandModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BrandModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Brand,Model,Description")] BrandModel brandModel)
        {
            if (ModelState.IsValid)
            {
                brandModel.RegesteredOn=DateTime.Now;

                _context.BrandModel.Add(brandModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brandModel);
        }

        // GET: BrandModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BrandModel == null)
            {
                return NotFound();
            }

            var brandModel = await _context.BrandModel.FindAsync(id);
            if (brandModel == null)
            {
                return NotFound();
            }
            return View(brandModel);
        }

        // POST: BrandModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,Description")] BrandModel brandModel)
        {
            if (id != brandModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    brandModel.RegesteredOn=DateTime.Now;
                    _context.BrandModel.Update(brandModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandModelExists(brandModel.Id))
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
            return View(brandModel);
        }

        // GET: BrandModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BrandModel == null)
            {
                return NotFound();
            }

            var brandModel = await _context.BrandModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brandModel == null)
            {
                return NotFound();
            }

            return View(brandModel);
        }

        // POST: BrandModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BrandModel == null)
            {
                return Problem("Entity set 'CarDbContext.BrandModel'  is null.");
            }
            var brandModel = await _context.BrandModel.FindAsync(id);
            if (brandModel != null)
            {
                _context.BrandModel.Remove(brandModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandModelExists(int id)
        {
          return (_context.BrandModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
