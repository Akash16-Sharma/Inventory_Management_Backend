using BackendAPI.Models.Roles;

namespace BackendAPI.IRepository.Roles
{
    public interface IRoles
    {
        List<Staff> GetStaff(int OrgId);
      int AddStaff(Staff staff);
        bool DeleteStaff(Staff staff);
        bool UpdateStaff(Staff staff);

        Staff CheckStaffType(int StaffId);


        public Staff GetStaffById(int id);
        bool AddAccess(Access access);
        bool UpdateAccess(Access access);
       List <Access> CheckAccess(int StaffId);

        List<Access> GetAccess(int StaffId);
        List<string> GetAccessNames(int StaffId);
       Staff  GetStaffByStaffId(int StaffId);
    }
}
