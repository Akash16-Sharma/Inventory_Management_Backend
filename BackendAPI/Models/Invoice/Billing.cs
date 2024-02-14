namespace BackendAPI.Models.Invoice
{
    public class Billing
    {
        public int Id { get; set; } 
        public int Customer_Id {  get; set; }
        public string Order_Id { get; set; }
        public float Amount {  get; set; }
        public int Org_Id { get; set; }
        public int Updated_By {  get; set; }
        public DateTime Inserted_On { get; set; }
    }
}
