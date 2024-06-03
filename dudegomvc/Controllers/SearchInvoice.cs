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
    public class SearchInvoice : Controller
    {
        private readonly ModelContext _context;

        public SearchInvoice(ModelContext context)
        {
            _context = context;
        }

        // GET: TbInvoices

        // GET: TbInvoices
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.TbInvoices
                .Include(t => t.IdBranchNavigation)
                .Include(t => t.IdCustomerNavigation)
                .Include(t => t.IdSalespersonNavigation);
            return View(await modelContext.ToListAsync());
        }


        /* public async Task<IActionResult> Index()
         {
             var modelContext = _context.TbInvoices.Include(t => t.IdBranchNavigation.NameBranch).Include(t => t.IdCustomerNavigation).Include(t => t.IdSalespersonNavigation);
             return View(await modelContext.ToListAsync());
         }

        */

    }
}
