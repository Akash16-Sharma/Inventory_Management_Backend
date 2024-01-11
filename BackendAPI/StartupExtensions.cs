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
            services.AddScoped<IItemRepo, ItemRepo>();
            services.AddScoped<IUnitType, UnitTypeRepo>();
            services.AddScoped<ICity_State, City_StateRepo>();
            services.AddScoped<ICategory, CategoryRepo>();
            services.AddScoped<IVendor, VendorRepo>();
            services.AddScoped<AppSettings>();
            services.AddScoped<User_Info>();
            services.AddScoped<EncDycPassword>();

            services.AddScoped<ItemWithImage>();
            services.AddScoped<Item>();
            services.AddScoped<UnitType>();
            services.AddScoped<Cities_List>();
            services.AddScoped<StateList>();
            services.AddScoped<Category>();
            services.AddScoped<Vendor>();


            //Services.AddScoped<EncDycPassword>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();

            return services;
        }
    }
}
