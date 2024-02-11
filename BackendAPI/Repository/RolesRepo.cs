using BackendAPI.AuthServices;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models;
using BackendAPI.Models.Roles;

namespace BackendAPI.Repository
{
    public class RolesRepo:IRoles
    {
        private readonly DataContext _datacontext;
        private readonly EncDycPassword _encDycPassword;

        public RolesRepo(DataContext datacontext,EncDycPassword encDycPassword)
        {
            _datacontext = datacontext;
            _encDycPassword = encDycPassword;
        }
       

        public List<Staff> GetStaff(int OrgId)
        {
           var data=_datacontext.Staff.Where(x=>x.OrgId == OrgId).ToList();
            return data;
        }

        public bool AddStaff(Staff staff)
        {
            string PassSecret = "b14ca5898a4e4133bbce2ea2315a1916";
            string password = _encDycPassword.EncryptPassword(PassSecret, staff.Password);
            staff.Password = password;
            _datacontext.Staff.Add(staff);
           int i= _datacontext.SaveChanges();
            if(i>0)
            {
                return true;
            }
            else { return false; }

        }

        public bool DeleteStaff(Staff staff)
        {
            var DelData=_datacontext.Staff.Where(x=>x.Id== staff.Id).FirstOrDefault();
            if(DelData!=null)
            {
                DelData.IsActive = false;
                DelData.InsertedOn=DateTime.Now;
                _datacontext.Staff.Update(staff);
                _datacontext.SaveChanges();
                return true;
            }
            else 
                return false;
        }

        bool IRoles.UpdateStaff(Staff staff)
        {
            string PassSecret = "b14ca5898a4e4133bbce2ea2315a1916";
            string password = _encDycPassword.EncryptPassword(PassSecret, staff.Password);
            var UpdData = _datacontext.Staff.Where(x => x.Id == staff.Id).FirstOrDefault();
            UpdData.Staff_Name = staff.Staff_Name;
            UpdData.OrgId = staff.OrgId;
            UpdData.RoleType = staff.RoleType;
            UpdData.Email = staff.Email;
            UpdData.Password = password;
            UpdData.InsertedOn = DateTime.Now;
            _datacontext.Staff.Update(UpdData);
            int i = _datacontext.SaveChanges();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddAccess(Access access)//You Can Call It Update Access
        {
            access.IsActive = true;
            access.InsertedOn = DateTime.Now;
           _datacontext.StaffAccess.Add(access);
          int i=  _datacontext.SaveChanges();
            if(i > 0)
            {
                return true;
            }
            else { return false; }

        }

        public List<Access> CheckAccess(int StaffId)
        {
            var data=_datacontext.StaffAccess.Where(x=>x.StaffId == StaffId).ToList();
            return data;
        }

        public bool UpdateAccess(Access access)
        {
            var Update_Data=_datacontext.StaffAccess.Where(x=>x.StaffId==access.StaffId).ToList();
            return true;
        }

        public Staff CheckStaffType(int StaffId)
        {
            
            var data=_datacontext.Staff.Where(x=>x.Id==StaffId).FirstOrDefault();
            return data;
        }
    }
}
