namespace BackendAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime Inserted_On { get; set; }
        public int Updated_By { get; set; }
        public int OrgId { get; set; }
    }
}
