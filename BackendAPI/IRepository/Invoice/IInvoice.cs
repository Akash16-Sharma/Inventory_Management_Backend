using BackendAPI.Models.Invoice;

namespace BackendAPI.IRepository.Invoice
{
    public interface IInvoice
    {
        public bool AddBillingDetails(Billing bill);
    }
}
