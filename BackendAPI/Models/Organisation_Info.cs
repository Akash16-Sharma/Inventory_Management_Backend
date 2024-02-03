using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    public class Organisation_Info
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public int PinCode { get; set; }
    }
}
