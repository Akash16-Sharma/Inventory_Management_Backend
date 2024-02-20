using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Models
{
    public class IncOrderRequest
    {
       public List<IncOrderItem> OrderItems { get; set; }
       public Inc_Order Inc_Orders { get; set; }

        
    }
    public class Inc_Order
    {
        
        public int Id { get; set; }
        public int Vendor_Id { get; set; }
        //public List<OrderItem> OrderItems { get; set; }
        public int Item_Id { get; set; }
        public int Quantity { get; set; }
        public string Order_Date { get; set; }
        public string Expected_Date { get; set; }
       
       
        public DateTime Actual_Date { get; set; }
        public int Updated_By { get; set; }
        public DateTime Inserted_On { get; set; }
        public int OrgId { get; set; }
        public bool IsActive { get; set; }
        public string Purchase_Order_Id { get; set; }
       // public float PurchaseAmount { get; set; }
    }

    [Owned]
    public class IncOrderItem
    {
        public int Item_Id { get; set; }
        public int Quantity { get; set; }
    }
    
   
}
