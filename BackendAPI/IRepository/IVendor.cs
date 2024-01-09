using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IVendor
    {
       List <Vendor> GetVendor();
        bool AddVendor(Vendor vendor);
        bool RemoveVendor(int id);
        bool UpdateVendor(Vendor vendor);
    }
}
