using BackendAPI.Models.Roles;

namespace BackendAPI.IRepository.Roles
{
    public interface IRoles
    {
        List<Staff> GetStaff(int OrgId);
      bool AddStaff(Staff staff);
        bool DeleteStaff(Staff staff);
        bool UpdateStaff(Staff staff);

        Staff CheckStaffType(int StaffId);

       

        bool AddAccess(Access access);
        bool UpdateAccess(Access access);
       List <Access> CheckAccess(int StaffId);
    }
}
