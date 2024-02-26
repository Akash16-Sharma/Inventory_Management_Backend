namespace FrontEnd_View.Models
{
    public class SellInfoById
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime Actual_Date { get; set; }
        public DateTime Expected_Date { get; set; }
        public DateTime Order_Date { get; set; }
        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public decimal ItemSellingPrice { get; set; }
        public string Sell_Order_Id { get; set; }
        public int Vendor_Id { get; set; }
    }
}
