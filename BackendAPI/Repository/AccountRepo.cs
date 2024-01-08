using BackendAPI.AuthServices;
using BackendAPI.IRepository;
using BackendAPI.Models;
using BackendAPI.Models.Class;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendAPI.Repository
{
    public class AccountRepo:IUser_Login
    {

        private readonly DataContext _context;
        private readonly AppSettings _appsettings;
        EncDycPassword encypss =new EncDycPassword();

        public AccountRepo(DataContext context,AppSettings appsettings)
        {

            _context = context; 
            _appsettings = appsettings;

        }

        public User_Login Is_Login(string Username, string Password)
        {
            string PassSecret = "b14ca5898a4e4133bbce2ea2315a1916";
            string EncPss = encypss.EncryptPassword(PassSecret, Password);
            var data = _context.User_Login.Where(x => x.Username == Username && x.Password == EncPss&&x.ISActive==true).FirstOrDefault();
            if (data != null)
            {
                return data;
            }
            else { return null; }
        }

      
    }
}
