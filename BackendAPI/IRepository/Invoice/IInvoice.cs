using BackendAPI.Models.Invoice;

namespace BackendAPI.IRepository.Invoice
{
    public interface IInvoice
    {
        public Billing AddBillingDetails(Billing bill);
    }
}
