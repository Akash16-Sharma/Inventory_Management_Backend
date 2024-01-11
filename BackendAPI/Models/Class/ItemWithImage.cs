namespace BackendAPI.Models.Class
{
    public class ItemWithImage
    {
        public Item Item { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
