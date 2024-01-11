using BackendAPI.AuthServices;
using BackendAPI.IRepository;
using BackendAPI.Models;
using BackendAPI.Models.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static System.Net.WebRequestMethods;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUser_Login _User_Login;
        private readonly AppSettings _appSettings;
        private readonly IOrganisation_Info _organisation_info;
        private readonly ICity_State _city_state;

        private IAuthenticateService _authenticateService;
        public AccountController(IUser_Login User_Login, IOptions<AppSettings> appSettings, IAuthenticateService authenticateService, IOrganisation_Info organisation_info, ICity_State city_state)
        {

            _User_Login = User_Login;
            _appSettings = appSettings.Value;
            _authenticateService = authenticateService;
            _organisation_info = organisation_info;
            _city_state = city_state;
        }

     
        [HttpPost]
        public IActionResult AddOrganisation(AddAllInfo info)
        {
            bool Isadd = _organisation_info.AddOrganisation_Info(info);
            if (Isadd == true)
            {
                return Ok(new { Status = 200, Message = "Inserted Successfully" });
            }
            else
            {
                return Ok(new { Status = 400, Message = "BadRequest" });
            }
        }

        [HttpGet]
        public List<Organisation_Info> GetOrganisation()
        {
            return _organisation_info.GetOrganisation_Infos();
        }
        

        [HttpPost]
        public IActionResult login(User_Login loginData)
        {

            var data = _User_Login.Is_Login(loginData.Username, loginData.Password);

            var authUser = _authenticateService.Authenticate(loginData.Username, loginData.Password);
            if (authUser != null)
            {
                if (authUser == null)
                {
                    return Unauthorized(new { status = 401, message = "Username or Password is incorrect", value = "" });
                }
                else
                {
                    return Ok(new { status = 200, value = authUser });
                }
            }
            else
            {
                return Unauthorized(new { status = 401, message = "Username or Password is incorrect", value = "" });
            }
        }



        [HttpGet]
        public IActionResult Statelist()
        {
            var data = _city_state.GetAllStates();
            if(data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Citylist(int Stateid)
        {
            var data = _city_state.GetAllCities(Stateid);
            if(data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }


    }
}
