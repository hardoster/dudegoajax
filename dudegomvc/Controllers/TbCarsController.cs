using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dudegomvc.Models;

namespace dudegomvc.Controllers
{
    public class TbCarsController : Controller
    {
        private readonly ModelContext _context;

        public TbCarsController(ModelContext context)
        {
            _context = context;
        }

        // GET: TbCars
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbCars.ToListAsync());
        }

        // GET: TbCars/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCar = await _context.TbCars
                .FirstOrDefaultAsync(m => m.IdCar == id);
            if (tbCar == null)
            {
                return NotFound();
            }

            return View(tbCar);
        }

        // GET: TbCars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbCars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCar,NumberPlate,Brand,Model,Color,Year")] TbCar tbCar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbCar);
        }

        // GET: TbCars/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCar = await _context.TbCars.FindAsync(id);
            if (tbCar == null)
            {
                return NotFound();
            }
            return View(tbCar);
        }

        // POST: TbCars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdCar,NumberPlate,Brand,Model,Color,Year")] TbCar tbCar)
        {
            if (id != tbCar.IdCar)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCarExists(tbCar.IdCar))
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
            return View(tbCar);
        }

        // GET: TbCars/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCar = await _context.TbCars
                .FirstOrDefaultAsync(m => m.IdCar == id);
            if (tbCar == null)
            {
                return NotFound();
            }

            return View(tbCar);
        }

        // POST: TbCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var tbCar = await _context.TbCars.FindAsync(id);
            if (tbCar != null)
            {
                _context.TbCars.Remove(tbCar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbCarExists(decimal id)
        {
            return _context.TbCars.Any(e => e.IdCar == id);
        }
    }
}
