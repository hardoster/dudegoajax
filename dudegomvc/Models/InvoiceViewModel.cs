namespace dudegomvc.Models
{
    public class InvoiceViewModel
    {
        public TbInvoice Invoice { get; set; }
        public TbInvoiceDetail InvoiceDetails { get; set; }

        public TbCar InvoiceCar { get; set; }
    }
}
