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
    public class TbBranchesController : Controller
    {
        private readonly ModelContext _context;

        public TbBranchesController(ModelContext context)
        {
            _context = context;
        }

        // GET: TbBranches
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbBranches.ToListAsync());
        }

        // GET: TbBranches/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbBranch = await _context.TbBranches
                .FirstOrDefaultAsync(m => m.IdBranch == id);
            if (tbBranch == null)
            {
                return NotFound();
            }

            return View(tbBranch);
        }

        // GET: TbBranches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbBranches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBranch,NameBranch,Address,PhoneNumberBranch")] TbBranch tbBranch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbBranch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbBranch);
        }

        // GET: TbBranches/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbBranch = await _context.TbBranches.FindAsync(id);
            if (tbBranch == null)
            {
                return NotFound();
            }
            return View(tbBranch);
        }

        // POST: TbBranches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdBranch,NameBranch,Address,PhoneNumberBranch")] TbBranch tbBranch)
        {
            if (id != tbBranch.IdBranch)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbBranch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbBranchExists(tbBranch.IdBranch))
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
            return View(tbBranch);
        }

        // GET: TbBranches/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbBranch = await _context.TbBranches
                .FirstOrDefaultAsync(m => m.IdBranch == id);
            if (tbBranch == null)
            {
                return NotFound();
            }

            return View(tbBranch);
        }

        // POST: TbBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var tbBranch = await _context.TbBranches.FindAsync(id);
            if (tbBranch != null)
            {
                _context.TbBranches.Remove(tbBranch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbBranchExists(decimal id)
        {
            return _context.TbBranches.Any(e => e.IdBranch == id);
        }
    }
}
