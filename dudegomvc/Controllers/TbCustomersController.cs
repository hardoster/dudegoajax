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
    public class TbCustomersController : Controller
    {
        private readonly ModelContext _context;

        public TbCustomersController(ModelContext context)
        {
            _context = context;
        }

        // GET: TbCustomers
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbCustomers.ToListAsync());
        }

        // GET: TbCustomers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCustomer = await _context.TbCustomers
                .FirstOrDefaultAsync(m => m.IdCustomer == id);
            if (tbCustomer == null)
            {
                return NotFound();
            }

            return View(tbCustomer);
        }

        // GET: TbCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCustomer,DuiCustomer,DrivingLic,NameCustomer,PhoneNumber,Address")] TbCustomer tbCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbCustomer);
        }

        // GET: TbCustomers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCustomer = await _context.TbCustomers.FindAsync(id);
            if (tbCustomer == null)
            {
                return NotFound();
            }
            return View(tbCustomer);
        }

        // POST: TbCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdCustomer,DuiCustomer,DrivingLic,NameCustomer,PhoneNumber,Address")] TbCustomer tbCustomer)
        {
            if (id != tbCustomer.IdCustomer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCustomerExists(tbCustomer.IdCustomer))
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
            return View(tbCustomer);
        }

        // GET: TbCustomers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCustomer = await _context.TbCustomers
                .FirstOrDefaultAsync(m => m.IdCustomer == id);
            if (tbCustomer == null)
            {
                return NotFound();
            }

            return View(tbCustomer);
        }

        // POST: TbCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var tbCustomer = await _context.TbCustomers.FindAsync(id);
            if (tbCustomer != null)
            {
                _context.TbCustomers.Remove(tbCustomer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbCustomerExists(decimal id)
        {
            return _context.TbCustomers.Any(e => e.IdCustomer == id);
        }
    }
}
