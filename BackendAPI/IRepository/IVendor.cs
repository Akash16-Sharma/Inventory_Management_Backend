using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IVendor
    {
       List <Vendor> GetVendor(int OrgId);
        bool AddVendor(Vendor vendor);
        bool RemoveVendor(Vendor vendor);
        bool UpdateVendor(Vendor vendor);
    }
}
