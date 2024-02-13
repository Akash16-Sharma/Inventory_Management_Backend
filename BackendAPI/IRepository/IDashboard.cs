using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IDashboard
    {
        List<Out_Order> TotalOut_Order(int orgid);
        List<Out_Order> HighestSellingItem(int orgid);
        List<Inc_Order> TotalIncOrder(int orgid);
        List<Inc_Order> HighestPurchasingItem(int orgid);
        List<Item> TotalItems(int orgid);
        List<Item> LowStockItem(int orgid);
        
    }
}
