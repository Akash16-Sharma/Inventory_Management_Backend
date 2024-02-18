namespace BackendAPI.Models.Invoice
{
    public class Billing
    {
        public int Id { get; set; } 
        public int Customer_Id {  get; set; }
        public int Invoice_No {  get; set; }
        public string Order_Id { get; set; }
        public float Amount {  get; set; }
        public int Org_Id { get; set; }
        public int Updated_By {  get; set; }
        public DateTime Inserted_On { get; set; }
        public string Discount { get; set; }
        public string BillingAddress { get; set;}
    }
}
