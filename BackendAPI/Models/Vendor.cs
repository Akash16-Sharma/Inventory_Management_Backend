namespace BackendAPI.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public int UpdatedBy { get; set; }
        public int OrgId { get; set; }

    }
    
}
