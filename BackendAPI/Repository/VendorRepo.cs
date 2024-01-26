using BackendAPI.IRepository;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class VendorRepo:IVendor
    {
        private readonly DataContext _Vendor;
        public VendorRepo(DataContext vendor)
        {
            _Vendor = vendor;
        }

        public bool AddVendor(Vendor vendor)
        {
            vendor.IsActive=true;
            vendor.InsertedOn=DateTime.Now;
            _Vendor.Vendor.Add(vendor);
           int IsSaved= _Vendor.SaveChanges();
            if(IsSaved>0)
            {
                return true;
            }
            else { return false; }
        }

        public List<Vendor> GetVendor(int OrgId)
        {
            var data=_Vendor.Vendor.Where(x =>x.OrgId==OrgId&& x.IsActive==true).ToList();
            return data;
        }

        public bool RemoveVendor(Vendor vendor)
        {
            var data = _Vendor.Vendor.Where(x => x.Id == vendor.Id).FirstOrDefault();
            data.IsActive=false;
            data.InsertedOn=DateTime.Now;
            _Vendor.Vendor.Update(data);
            _Vendor.SaveChanges();
            return true;
        }

        public bool UpdateVendor(Vendor vendor)
        {
            vendor.InsertedOn= DateTime.Now;
            var Vendordata=_Vendor.Vendor.Where(x => x.Id==vendor.Id).FirstOrDefault(); 
            Vendordata.Name=vendor.Name;
            Vendordata.Email=vendor.Email;
            Vendordata.Phone=vendor.Phone;
            _Vendor.Vendor.Update(Vendordata);
            _Vendor.SaveChanges();
            return true;
        }
    }
}
