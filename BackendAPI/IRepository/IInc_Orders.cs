using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IInc_Orders
    {
        List<Object> GetOrderInfoByPurchase(string Purchase_Order_Id);
        List<Object> GetOrderInfo(int orgid);
        bool AddOrder(Inc_Order order);
        bool UpdateOrder(Inc_Order order);
        bool DeleteOrder(int id,int staffid);
        Inc_Order GetOrderInfoById(int id);
    }
}
