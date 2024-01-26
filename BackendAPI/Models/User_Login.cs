namespace BackendAPI.Models
{
    public class User_Login
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int OrgId { get; set; }
        public DateTime Inserted_On { get; set; }
        public bool ISActive { get; set; }
    }
}
