using BackendAPI.AuthServices;
using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models;
using BackendAPI.Models.Class;
using BackendAPI.Models.Roles;
using BackendAPI.Repository;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.Extensions.DependencyInjection;
using BackendAPI.Models.Invoice;
using BackendAPI.IRepository.Invoice;
using BackendAPI.Repository.Invoice;

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
            services.AddScoped<IRoles, RolesRepo>();
            services.AddScoped<IInc_Orders,Inc_OrderRepo>();
            services.AddScoped<IOut_Order,Out_OrderRepo>();
            services.AddScoped<ICustomer,CustomerRepo>();
            services.AddScoped<IInvoice,InvoiceRepository>();
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
            services.AddScoped<Staff>();
            services.AddScoped<Access>();
            services.AddScoped<Controller_Functions>();
            services.AddScoped<Excel>();
            services.AddScoped<Inc_Order>();
            services.AddScoped<Out_Order>();
            services.AddScoped<Customer>();
            services.AddScoped<InvoiceRequest>();
            services.AddScoped<Billing>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));



            //Services.AddScoped<EncDycPassword>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();

            return services;
        }
    }
}
