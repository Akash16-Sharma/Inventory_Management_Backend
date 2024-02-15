using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IOut_Order
    {
        List<Object> GetOutOrdersBySalesOrderID(string SalesOrderID);
        List<Object> GetOutOrderInfo(int orgid);
        bool AddOrder(Out_Order order);
        bool UpdateOrder(Out_Order order);
        bool DeleteOrder(int Id,int StaffId);
        Out_Order GetOrderById(int id);
    }
}
