namespace BackendAPI.Models
{
    public class User_Info
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime InsertedOn { get; set; }
        public bool IsActive { get; set; }
        public int OrgId { get; set; }
    }
}
