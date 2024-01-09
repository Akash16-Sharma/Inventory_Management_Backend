namespace BackendAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertedOn { get; set; }
        public int UpdatedBy {  get; set; }
    }
}
