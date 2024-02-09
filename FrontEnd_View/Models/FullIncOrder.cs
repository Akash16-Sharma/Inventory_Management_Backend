using BackendAPI.Models;

namespace FrontEnd_View.Models
{
    public class FullIncOrder
    {
        public int VendorId { get; set; }
        public string Date { get; set; }
        public string PurchaseOrder { get; set; }
        public List<IncItemsDetail> Items { get; set; }
    }
}
