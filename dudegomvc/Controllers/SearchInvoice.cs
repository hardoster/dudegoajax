using dudegomvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dudegomvc.Controllers
{
    public class SearchInvoice : Controller
    {

        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var invoices = db.TbInvoices.ToList(); // Assuming db.TbInvoices returns a list of TbInvoice
            return View(invoices); // Passing the correct model type
        }
    }
}
