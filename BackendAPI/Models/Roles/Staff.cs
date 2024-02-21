namespace BackendAPI.Models.Roles
{
    public class Staff
    {
        public int Id { get; set; }
        public string Staff_Name { get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
        public string RoleType { get; set; }
        public int OrgId {  get; set; }
        public int Updated_By {  get; set; }
        public bool IsActive { get; set; }  
        public DateTime InsertedOn {  get; set; }

    }
    public class StaffRoleRequest
    {
        public Staff Staff { get; set; }
        public List<Access> StaffAccess { get; set; }
    }

}
