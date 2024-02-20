using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Models
{
    public class Out_OrderRequest
    {
        public List<OutOrderItem>OrderItems { get; set; }
        public Out_Order order { get; set; }
        // Additional properties for frontend-provided dates as strings
        public string OrderDate { get; set; }
        public string ExpectedDate { get; set; }
    }
    public class Out_Order
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int Item_Id { get; set; }
        public int Quantity { get; set; }
        public DateOnly Expected_Date { get; set; }
        public DateTime Actual_Date { get; set; }
        public int Updated_By { get; set; }
        public DateTime Inserted_On { get; set; }
        public DateOnly Order_Date { get; set; }
        public int OrgId { get; set; }
        public bool IsActive { get; set; }
        public string Sales_Order_Id { get; set; }
       // public float SellAmount { get; set; }
    }
    [Owned]
    public class OutOrderItem
    {
        public int Item_Id { get; set; }
        public int Quantity { get; set; }
    }
}
