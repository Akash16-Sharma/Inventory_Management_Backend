using BackendAPI.Models;
using BackendAPI.Models.Roles;

namespace BackendAPI.IRepository
{
    public interface IUser_Login
    {
        User_Login Is_Login(string Email,string Password);

        Staff CheckStaff(string Email,string Password);

        
    }
}
