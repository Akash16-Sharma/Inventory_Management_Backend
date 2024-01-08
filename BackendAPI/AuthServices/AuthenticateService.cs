using BackendAPI.IRepository;
using BackendAPI.Models;
using BackendAPI.Models.Class;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendAPI.AuthServices
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        private readonly IUser_Login _User_Login;


        string JwtSecKey = "D7r7aTSsytKLxhwkSB5AhfXzEaz7SWuj";


        public AuthenticateService(IOptions<AppSettings> appSettings, IUser_Login User_Login)
        {
            _appSettings = appSettings.Value;
            _User_Login = User_Login;
        }



        public InfoClass Authenticate(string Username, string Password)
        {
            var user = _User_Login.Is_Login(Username, Password);

            if (user == null)
            {
                return null;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtsecKey = Encoding.ASCII.GetBytes(JwtSecKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtsecKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var authToken = tokenHandler.CreateToken(tokenDescriptor);

                return new InfoClass
                {
                    auth_token = tokenHandler.WriteToken(authToken),
                    
                };
            }
        }



    }
}
