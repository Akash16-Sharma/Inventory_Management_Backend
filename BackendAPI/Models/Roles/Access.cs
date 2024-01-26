namespace BackendAPI.Models.Roles
{
    public class Access
    {
        public int Id { get; set; }
        public string SideBarName { get; set; }
        public bool IsCreate { get; set; }
        public bool IsShow { get; set; }
        public bool IsModify { get; set; }
        public bool Import { get; set; }
        public bool Export { get; set; }
        public DateTime InsertedOn { get; set; }
        public bool IsActive { get; set; }
        public int Updated_By {  get; set; }
        public int StaffId { get; set;}

    }
}
