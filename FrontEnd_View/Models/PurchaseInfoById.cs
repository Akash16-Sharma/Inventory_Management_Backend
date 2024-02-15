namespace FrontEnd_View.Models
{
    public class PurchaseInfoById
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime Actual_Date { get; set; }
        public DateTime Expected_Date { get; set; }
        public DateTime Order_Date { get; set; }
        public string VendorName { get; set; }
        public string ItemName { get; set; }
        public decimal ItemBuyingPrice { get; set; }
        public string Purchase_Order_Id { get; set; }
        public int Vendor_Id { get; set; }
    }
}
