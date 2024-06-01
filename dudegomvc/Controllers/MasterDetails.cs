using dudegomvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace dudegomvc.Controllers
{
    [Route("MasterDetails")]
    public class MasterDetails : Controller
    {
        private readonly ModelContext _context;

        public MasterDetails(ModelContext context)
        {
            _context = context;
        }

        // GET: TbInvoices/Create
        public IActionResult Create()
        {
            var lastIdInvoice = _context.TbInvoices.OrderByDescending(i => i.IdInvoice).FirstOrDefault()?.IdInvoice;
            ViewData["IdInvoice"] = lastIdInvoice.ToString(); 

            var lastNumberInvoice = _context.TbInvoices.OrderByDescending(i => i.InvoiceNumber).FirstOrDefault()?.InvoiceNumber;
            ViewData["InvoiceNumber"] = lastNumberInvoice.ToString(); // Convierte a string si el input espera un string

            var lastNameBranch = _context.TbBranches.OrderByDescending(tb => tb.IdBranch).FirstOrDefault()?.NameBranch;
            ViewData["NameBranch"] = lastNameBranch;

            var lastNameCustomer = _context.TbCustomers.OrderByDescending(tb => tb.IdCustomer).FirstOrDefault()?.NameCustomer;
            ViewData["NameCustomer"] = lastNameCustomer;

            var lastNameSalesPerson = _context.TbSalespeople.OrderByDescending(tb => tb.IdSalesperson).FirstOrDefault()?.NameSalesperson;
            ViewData["NameSalesperson"] = lastNameSalesPerson;

            var cars = _context.TbCars.ToList(); 
            ViewBag.Cars = cars;

            return View();
        }


                [HttpPost("ListDetail78")]
                public async Task<IActionResult> ListDetail78([FromBody] List<TbInvoiceDetail> dataToSend)
                {

                    if (ModelState.IsValid)
                    {
                        foreach (var detail in dataToSend)
                        {
                            var tbInvoiceDetail = new TbInvoiceDetail
                            {
                                IdInvoice = detail.IdInvoice,
                                IdCar = detail.IdCar,
                                DayDuration = detail.DayDuration,
                                PriceDay = detail.PriceDay,
                                RentalDeposit = detail.RentalDeposit
                            };

                            _context.Add(tbInvoiceDetail);
                        }

                        await _context.SaveChangesAsync();
         return RedirectToAction("Index", "SearchInvoice");

            }

            return BadRequest("Error en los datos enviados");
                }


                

        
                [HttpPost]
                [ValidateAntiForgeryToken]

                public async Task<IActionResult> Create(InvoiceViewModel viewModel)
                {
                    if (ModelState.IsValid)
                    {
                        // Mapea los datos de viewModel a un objeto TbInvoice
                        var tbInvoice = new TbInvoice
                        {
                            InvoiceNumber = viewModel.Invoice.InvoiceNumber,
                            IdCustomer = viewModel.Invoice.IdCustomer,
                            IdBranch = viewModel.Invoice.IdBranch,
                            IdSalesperson = viewModel.Invoice.IdSalesperson
                            // Añade más mapeos según sea necesario
                        };

                        // Guarda la factura principal en la base de datos
                        _context.Add(tbInvoice);
                        await _context.SaveChangesAsync();

                        // Redirige a la acción Index después de guardar los datos
                        return RedirectToAction(nameof(Index));
                    }

                    // Si el modelo no es válido, recarga los datos en ViewData y vuelve a mostrar la vista
                    ViewData["IdBranch"] = new SelectList(_context.TbBranches, "IdBranch", "IdBranch", viewModel.Invoice.IdBranch);
                    ViewData["IdCustomer"] = new SelectList(_context.TbCustomers, "IdCustomer", "IdCustomer", viewModel.Invoice.IdCustomer);
                    ViewData["IdSalesperson"] = new SelectList(_context.TbSalespeople, "IdSalesperson", "IdSalesperson", viewModel.Invoice.IdSalesperson);

                    // Retorna la vista con el ViewModel
                    return View(viewModel);

                }

    }
}
