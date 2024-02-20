using System.Text.RegularExpressions;

namespace FrontEnd_View.Models
{
    public class Browser
    {
        public string userAgent { get; set; }
        public Regex OS { get; set; }
        public Regex device { get; set; }
    }
}
