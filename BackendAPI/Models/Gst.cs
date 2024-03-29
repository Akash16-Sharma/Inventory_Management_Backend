namespace BackendAPI.Models
{
    public class Gst
    {
        public int Id { get; set; }
        public string TaxName { get; set; }
        public decimal Tax_Percent { get; set; }
        public int OrgId { get; set; }
        public int Updated_By { get; set; }
        public DateTime Inserted_On { get; set; }
    }
}
