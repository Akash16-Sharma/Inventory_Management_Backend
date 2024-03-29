namespace BackendAPI.Models
{
    public class Hsn
    {
        public int Id { get; set; }
        public string HSNCODE { get; set; }
        public int GSTID { get; set; }
        public int OrgId { get; set; }
        public int Updated_By { get; set; }
        public DateTime Inserted_On { get; set; }
    }
}
