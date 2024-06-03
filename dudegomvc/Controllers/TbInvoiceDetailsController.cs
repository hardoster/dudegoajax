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
    public class TbInvoiceDetailsController : Controller
    {
        private readonly ModelContext _context;

        public TbInvoiceDetailsController(ModelContext context)
        {
            _context = context;
        }

        // GET: TbInvoiceDetails
        public async Task<IActionResult> Index(decimal? id)
        {
            var invoice = await _context.TbInvoices
                .Include(t => t.IdBranchNavigation)
                .Include(t => t.TbInvoiceDetails) // Ajusta aquí a tu propiedad de navegación
                      .ThenInclude(d => d.IdCarNavigation)
                .Include(t => t.IdCustomerNavigation)
                .Include(t => t.IdSalespersonNavigation)
                .FirstOrDefaultAsync(m => m.IdInvoice == id);

            if (invoice == null)
            {
                return NotFound();
            }
            var invoiceDetails = await _context.TbInvoiceDetails
           .Where(d => d.IdInvoice == id)
           .ToListAsync();

            ViewData["InvoiceNumber"] = invoice.InvoiceNumber;
            ViewData["NameCustomer"] = invoice.IdCustomerNavigation.NameCustomer;
            ViewData["NameBranch"] = invoice.IdBranchNavigation.NameBranch;
            ViewData["NameSalesperson"] = invoice.IdSalespersonNavigation.NameSalesperson;

            return View(invoiceDetails);

        }
        /*
                // GET: TbInvoiceDetails/Details/5
                public async Task<IActionResult> Details(decimal? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var tbInvoiceDetail = await _context.TbInvoiceDetails
                        .Include(t => t.IdCarNavigation)
                        .Include(t => t.IdInvoiceNavigation)
                        .FirstOrDefaultAsync(m => m.IdInvoiceDetails == id);
                    if (tbInvoiceDetail == null)
                    {
                        return NotFound();
                    }

                    return View(tbInvoiceDetail);
                }

                // GET: TbInvoiceDetails/Create
                public IActionResult Create()
                {
                    ViewData["IdCar"] = new SelectList(_context.TbCars, "IdCar", "IdCar");
                    ViewData["IdInvoice"] = new SelectList(_context.TbInvoices, "IdInvoice", "IdInvoice");
                    return View();
                }

                // POST: TbInvoiceDetails/Create
                // To protect from overposting attacks, enable the specific properties you want to bind to.
                // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create([Bind("IdInvoiceDetails,IdInvoice,IdCar,DayDuration,PriceDay,RentalDeposit")] TbInvoiceDetail tbInvoiceDetail)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(tbInvoiceDetail);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    ViewData["IdCar"] = new SelectList(_context.TbCars, "IdCar", "IdCar", tbInvoiceDetail.IdCar);
                    ViewData["IdInvoice"] = new SelectList(_context.TbInvoices, "IdInvoice", "IdInvoice", tbInvoiceDetail.IdInvoice);
                    return View(tbInvoiceDetail);
                }

                // GET: TbInvoiceDetails/Edit/5
                public async Task<IActionResult> Edit(decimal? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var tbInvoiceDetail = await _context.TbInvoiceDetails.FindAsync(id);
                    if (tbInvoiceDetail == null)
                    {
                        return NotFound();
                    }
                    ViewData["IdCar"] = new SelectList(_context.TbCars, "IdCar", "IdCar", tbInvoiceDetail.IdCar);
                    ViewData["IdInvoice"] = new SelectList(_context.TbInvoices, "IdInvoice", "IdInvoice", tbInvoiceDetail.IdInvoice);
                    return View(tbInvoiceDetail);
                }

                // POST: TbInvoiceDetails/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to.
                // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(decimal id, [Bind("IdInvoiceDetails,IdInvoice,IdCar,DayDuration,PriceDay,RentalDeposit")] TbInvoiceDetail tbInvoiceDetail)
                {
                    if (id != tbInvoiceDetail.IdInvoiceDetails)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(tbInvoiceDetail);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!TbInvoiceDetailExists(tbInvoiceDetail.IdInvoiceDetails))
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
                    ViewData["IdCar"] = new SelectList(_context.TbCars, "IdCar", "IdCar", tbInvoiceDetail.IdCar);
                    ViewData["IdInvoice"] = new SelectList(_context.TbInvoices, "IdInvoice", "IdInvoice", tbInvoiceDetail.IdInvoice);
                    return View(tbInvoiceDetail);
                }

                // GET: TbInvoiceDetails/Delete/5
                public async Task<IActionResult> Delete(decimal? id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var tbInvoiceDetail = await _context.TbInvoiceDetails
                        .Include(t => t.IdCarNavigation)
                        .Include(t => t.IdInvoiceNavigation)
                        .FirstOrDefaultAsync(m => m.IdInvoiceDetails == id);
                    if (tbInvoiceDetail == null)
                    {
                        return NotFound();
                    }

                    return View(tbInvoiceDetail);
                }

                // POST: TbInvoiceDetails/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(decimal id)
                {
                    var tbInvoiceDetail = await _context.TbInvoiceDetails.FindAsync(id);
                    if (tbInvoiceDetail != null)
                    {
                        _context.TbInvoiceDetails.Remove(tbInvoiceDetail);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                private bool TbInvoiceDetailExists(decimal id)
                {
                    return _context.TbInvoiceDetails.Any(e => e.IdInvoiceDetails == id);
                }*/
    }
}
