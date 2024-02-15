using BackendAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ClosedXML.Excel;
using static BackendAPI.Models.Class.Controller_Functions;
using BackendAPI.Models.Class;
namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [Obsolete]
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private IConfiguration _configuration;
        private DataContext _entity;
        private readonly Controller_Functions _functions;

        [Obsolete]
        public ReportController(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IConfiguration configuration, DataContext entity,Controller_Functions functions)
        {
            _environment = environment;
            _configuration = configuration;
            _entity = entity;
            _functions = functions;
        }


        [HttpPost]
        [Route("ImportExcelFile/{StaffId}/{Sidebarname}")]
        [Obsolete]
        public IActionResult ImportExcelFile(int StaffId, String Sidebarname, IFormFile File)
        {
           _functions.ImportExcel(StaffId,Sidebarname,File);
            return Ok();
        }

       
    }
}
