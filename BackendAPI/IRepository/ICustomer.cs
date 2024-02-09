using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface ICustomer
    {
        List<Customer> GetCustomer(int orgid);
        bool AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(int id,int StaffId);
        Customer GetCustomerById(int id);
    }
}
