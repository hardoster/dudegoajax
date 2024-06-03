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
            // Obtener el último ID de factura
            var lastIdInvoice = _context.TbInvoices.OrderByDescending(i => i.IdInvoice).FirstOrDefault()?.IdInvoice;
            ViewData["IdInvoice"] = lastIdInvoice?.ToString();

            // Obtener el último número de factura
            var lastNumberInvoice = _context.TbInvoices.OrderByDescending(i => i.InvoiceNumber).FirstOrDefault()?.InvoiceNumber;
            ViewData["InvoiceNumber"] = lastNumberInvoice;

            // Obtener el nombre de la última sucursal asociada al último IdInvoice
            var lastBranchId = _context.TbInvoices.OrderByDescending(i => i.IdInvoice).FirstOrDefault()?.IdBranch;
            var lastNameBranch = _context.TbBranches.FirstOrDefault(tb => tb.IdBranch == lastBranchId)?.NameBranch;
            ViewData["NameBranch"] = lastNameBranch;

            // Obtener el nombre del último cliente asociado al último IdInvoice
            var lastCustomerId = _context.TbInvoices.OrderByDescending(tb => tb.IdInvoice).FirstOrDefault()?.IdCustomer;
            var lastCustomer = _context.TbCustomers.FirstOrDefault(c => c.IdCustomer == lastCustomerId);
            ViewData["NameCustomer"] = lastCustomer?.NameCustomer;

            // Obtener el nombre del último vendedor asociado al último IdInvoice
            var lastSalesPersonId = _context.TbInvoices.OrderByDescending(tb => tb.IdInvoice).FirstOrDefault()?.IdSalesperson;
            var lastSalesPerson = _context.TbSalespeople.FirstOrDefault(sp => sp.IdSalesperson == lastSalesPersonId);
            ViewData["NameSalesperson"] = lastSalesPerson?.NameSalesperson;

            // Obtener la lista de coches
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
                return RedirectToAction("Index", "Menu");

            }

            return BadRequest("Error en los datos enviados");
                }


                

        /*
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

                }*/

    }
}
