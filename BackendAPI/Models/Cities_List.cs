using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    public class Cities_List
    {
        
        public int Id { get; set; }

        
        public string Name { get; set; }

       
        public int StateID { get; set; }

        
        public DateTime InsertedOn { get; set; }
    }
}
