namespace BackendAPI.Models.Invoice
{
    public class InvoiceRequest
    {
        public string Name { get; set; }
        public Double Amount { get; set; }
        public string Item_Name { get; set; }
        public int Quantity { get; set; }
        public double Buying_Price { get; set; }


    }
}
