namespace BackendAPI.Models
{
    public class Out_Order
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int Item_Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Expected_Date { get; set; }
        public DateTime Actual_Date { get; set; }
        public int Updated_By { get; set; }
        public DateTime Inserted_On { get; set; }
        public int OrgId { get; set; }
        public bool IsActive { get; set; }
    }
}
