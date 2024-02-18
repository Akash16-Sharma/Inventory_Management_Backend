using BackendAPI.IRepository.Invoice;
using BackendAPI.Models;
using BackendAPI.Models.Invoice;

namespace BackendAPI.Repository.Invoice
{
    public class InvoiceRepository:IInvoice
    {
        private readonly DataContext _context;
        public InvoiceRepository(DataContext context)
        {
            _context = context;
        }

        public Billing AddBillingDetails(Billing bill)
        {
            Random r= new Random();
            bill.Invoice_No= r.Next();
           bill.Inserted_On=DateTime.Now;
            _context.Billing.Add(bill);
            int i=_context.SaveChanges();
            if(i>0)
            {
                return bill;
            }
            return null;
        }
    }
}
