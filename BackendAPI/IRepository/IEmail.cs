using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.IRepository
{
    public interface IEmail
    {
         Task<bool> SendEmailAsync(EmailModel emailModel);
    }
}
