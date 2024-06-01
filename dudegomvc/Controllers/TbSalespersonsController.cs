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
    public class TbSalespersonsController : Controller
    {
        private readonly ModelContext _context;

        public TbSalespersonsController(ModelContext context)
        {
            _context = context;
        }

        // GET: TbSalespersons
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbSalespeople.ToListAsync());
        }

        // GET: TbSalespersons/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSalesperson = await _context.TbSalespeople
                .FirstOrDefaultAsync(m => m.IdSalesperson == id);
            if (tbSalesperson == null)
            {
                return NotFound();
            }

            return View(tbSalesperson);
        }

        // GET: TbSalespersons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbSalespersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSalesperson,NameSalesperson,CodeSalesperson")] TbSalesperson tbSalesperson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbSalesperson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbSalesperson);
        }

        // GET: TbSalespersons/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSalesperson = await _context.TbSalespeople.FindAsync(id);
            if (tbSalesperson == null)
            {
                return NotFound();
            }
            return View(tbSalesperson);
        }

        // POST: TbSalespersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdSalesperson,NameSalesperson,CodeSalesperson")] TbSalesperson tbSalesperson)
        {
            if (id != tbSalesperson.IdSalesperson)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbSalesperson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbSalespersonExists(tbSalesperson.IdSalesperson))
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
            return View(tbSalesperson);
        }

        // GET: TbSalespersons/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSalesperson = await _context.TbSalespeople
                .FirstOrDefaultAsync(m => m.IdSalesperson == id);
            if (tbSalesperson == null)
            {
                return NotFound();
            }

            return View(tbSalesperson);
        }

        // POST: TbSalespersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var tbSalesperson = await _context.TbSalespeople.FindAsync(id);
            if (tbSalesperson != null)
            {
                _context.TbSalespeople.Remove(tbSalesperson);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbSalespersonExists(decimal id)
        {
            return _context.TbSalespeople.Any(e => e.IdSalesperson == id);
        }
    }
}
