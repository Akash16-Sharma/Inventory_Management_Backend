using BackendAPI.IRepository;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace BackendAPI.Repository
{
    public class EmailRepo : IEmail
    {
        public async Task<bool> SendEmailAsync(EmailModel emailModel)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    var credentials = new NetworkCredential("ninjainventory@gmail.com", "tdjelnusbtibtomj"); // Replace with your email credentials
                    client.Host = "smtp.gmail.com"; // Replace with your SMTP server host
                    client.Port = 465; // Replace with your SMTP port
                    client.EnableSsl = true; // Enable SSL if required

                    client.UseDefaultCredentials = false;
                    client.Credentials = credentials;

                    var message = new MailMessage
                    {
                        From = new MailAddress("ninjainventory@gmail.com"), // Replace with your email address
                        Subject = emailModel.Subject,
                        Body = emailModel.Body,
                        IsBodyHtml = true
                    };

                    message.To.Add(emailModel.To);

                    await client.SendMailAsync(message);

                    return true;
                }
            }
            catch ( )
            {
                return false;
            }
        
    }
    }
}
