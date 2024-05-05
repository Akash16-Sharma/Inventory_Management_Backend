namespace BackendAPI.Models
{
    public class History
    {
        public int Id { get; set; }
        public int StaffId {  get; set; }
        public int OrgId {  get; set; }
        public string Action {  get; set; }
        public string Controller_Name {  get; set; }
    }
}
