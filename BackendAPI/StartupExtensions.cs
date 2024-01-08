using BackendAPI.AuthServices;
using BackendAPI.IRepository;
using BackendAPI.Models;
using BackendAPI.Models.Class;
using BackendAPI.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BackendAPI
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddInterfaceRepo(this IServiceCollection services)
        {
            services.AddScoped<IUser_Login, AccountRepo>();
            services.AddScoped<IOrganisation_Info, Organisation_InfoRepo>();
            services.AddScoped<AppSettings>();
            services.AddScoped<User_Info>();
            services.AddScoped<EncDycPassword>();
            //Services.AddScoped<EncDycPassword>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();

            return services;
        }
    }
}
