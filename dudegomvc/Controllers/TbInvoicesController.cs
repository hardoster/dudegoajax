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
    public class TbInvoicesController : Controller
    {
        private readonly ModelContext _context;

        public TbInvoicesController(ModelContext context)
        {
            _context = context;
        }

        // GET: TbInvoices
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.TbInvoices
                 .Include(t => t.IdBranchNavigation) // Incluir toda la entidad Branch
                 .Include(t => t.IdCustomerNavigation) // Incluir toda la entidad Customer
                 .Include(t => t.IdSalespersonNavigation); // Incluir toda la entidad Salesperson
            return View(await modelContext.ToListAsync());
        }

        // GET: TbInvoices/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbInvoice = await _context.TbInvoices
                .Include(t => t.IdBranchNavigation)
                .Include(t => t.IdCustomerNavigation)
                .Include(t => t.IdSalespersonNavigation)
                .FirstOrDefaultAsync(m => m.IdInvoice == id);
            if (tbInvoice == null)
            {
                return NotFound();
            }

            return View(tbInvoice);
        }

        // GET: TbInvoices/Create
        public IActionResult Create()
        {
            ViewData["IdBranch"] = new SelectList(_context.TbBranches, "IdBranch", "NameBranch");
            ViewData["IdCustomer"] = new SelectList(_context.TbCustomers, "IdCustomer", "NameCustomer");
            ViewData["IdSalesperson"] = new SelectList(_context.TbSalespeople, "IdSalesperson", "CodeSalesperson");
            return View();
        }

        // POST: TbInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInvoice,InvoiceNumber,IdCustomer,IdBranch,IdSalesperson")] TbInvoice tbInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "MasterDetails");
            }
            ViewData["IdBranch"] = new SelectList(_context.TbBranches, "IdBranch", "IdBranch", tbInvoice.IdBranch);
            ViewData["IdCustomer"] = new SelectList(_context.TbCustomers, "IdCustomer", "IdCustomer", tbInvoice.IdCustomer);
            ViewData["IdSalesperson"] = new SelectList(_context.TbSalespeople, "IdSalesperson", "IdSalesperson", tbInvoice.IdSalesperson);
            return View(tbInvoice);
        }

        // GET: TbInvoices/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbInvoice = await _context.TbInvoices.FindAsync(id);
            if (tbInvoice == null)
            {
                return NotFound();
            }
            ViewData["IdBranch"] = new SelectList(_context.TbBranches, "IdBranch", "IdBranch", tbInvoice.IdBranch);
            ViewData["IdCustomer"] = new SelectList(_context.TbCustomers, "IdCustomer", "IdCustomer", tbInvoice.IdCustomer);
            ViewData["IdSalesperson"] = new SelectList(_context.TbSalespeople, "IdSalesperson", "IdSalesperson", tbInvoice.IdSalesperson);
            return View(tbInvoice);
        }

        // POST: TbInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdInvoice,InvoiceNumber,IdCustomer,IdBranch,IdSalesperson")] TbInvoice tbInvoice)
        {
            if (id != tbInvoice.IdInvoice)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbInvoiceExists(tbInvoice.IdInvoice))
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
            ViewData["IdBranch"] = new SelectList(_context.TbBranches, "IdBranch", "IdBranch", tbInvoice.IdBranch);
            ViewData["IdCustomer"] = new SelectList(_context.TbCustomers, "IdCustomer", "IdCustomer", tbInvoice.IdCustomer);
            ViewData["IdSalesperson"] = new SelectList(_context.TbSalespeople, "IdSalesperson", "IdSalesperson", tbInvoice.IdSalesperson);
            return View(tbInvoice);
        }

        // GET: TbInvoices/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbInvoice = await _context.TbInvoices
                .Include(t => t.IdBranchNavigation)
                .Include(t => t.IdCustomerNavigation)
                .Include(t => t.IdSalespersonNavigation)
                .FirstOrDefaultAsync(m => m.IdInvoice == id);
            if (tbInvoice == null)
            {
                return NotFound();
            }

            return View(tbInvoice);
        }

        // POST: TbInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var tbInvoice = await _context.TbInvoices.FindAsync(id);
            if (tbInvoice != null)
            {
                _context.TbInvoices.Remove(tbInvoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbInvoiceExists(decimal id)
        {
            return _context.TbInvoices.Any(e => e.IdInvoice == id);
        }
    }
}
