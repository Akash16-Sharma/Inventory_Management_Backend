using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IVendor
    {
       List <Vendor> GetVendor(int OrgId);
        bool AddVendor(Vendor vendor);
        bool RemoveVendor(int id,int StaffId);
        bool UpdateVendor(Vendor vendor);
        Vendor GetVendorById(int id);
    }
}
