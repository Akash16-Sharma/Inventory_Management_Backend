using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IUser_Login
    {
        User_Login Is_Login(string Username,string Password);
    }
}
