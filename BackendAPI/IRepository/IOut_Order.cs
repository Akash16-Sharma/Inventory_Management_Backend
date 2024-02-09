using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IOut_Order
    {
        List<Object> GetOutOrders(int OrgId);
        bool AddOrder(Out_Order order);
        bool UpdateOrder(Out_Order order);
        bool DeleteOrder(int Id,int StaffId);
        Out_Order GetOrderById(int id);
    }
}
