using BackendAPI.Models;
using BackendAPI.Models.Class;

namespace BackendAPI.AuthServices
{
    public interface IAuthenticateService
    {
        InfoClass Authenticate(string email, string Password);
    }
}
