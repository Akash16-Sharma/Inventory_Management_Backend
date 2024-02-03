namespace BackendAPI.Models
{
    public class AddAllInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Inserted_On { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public string PhoneNo { get; set; }
        public string Org_Email { get; set; }
        public string Type { get; set; }
       // public string Username { get; set; }
        public string Password { get; set; }
        public int PinCode { get; set; }
    }
}
