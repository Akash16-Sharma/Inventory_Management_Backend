using BackendAPI.AuthServices;
using BackendAPI.IRepository;
using BackendAPI.Models;
using BackendAPI.Models.Class;
using System;

namespace BackendAPI.Repository
{
    public class Organisation_InfoRepo : IOrganisation_Info
    {
        private readonly DataContext _context;
        private readonly EncDycPassword _encDycPassword;
        private readonly AppSettings _appSettings;

        public Organisation_InfoRepo(DataContext context, EncDycPassword encDycPassword, AppSettings appSettings)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _encDycPassword = encDycPassword ?? throw new ArgumentNullException(nameof(encDycPassword));
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public bool AddOrganisation_Info(AddAllInfo info)
        {
            info.InsertedOn = DateTime.Now;

            // Creating Organisation Info
            var orgInfo = new Organisation_Info
            {
                Name = info.Name,
                Address = info.Address,
                StateID = info.StateID,
                CityID = info.CityID,
                PhoneNo = info.PhoneNo,
                Email = info.Org_Email,
                Type = info.Type,
                IsActive = true,
                InsertedOn = info.InsertedOn,
            };

            _context.Organisation_Info.Add(orgInfo);
            _context.SaveChanges();

            // Creating User Login Info
            string PassSecret = "b14ca5898a4e4133bbce2ea2315a1916";
            var userLoginInfo = new User_Login
            {
                Username = info.Username,
                Inserted_On = info.InsertedOn,
                ISActive = true,
                
                Password = _encDycPassword.EncryptPassword(PassSecret, info.Password)
                
            };

            _context.User_Login.Add(userLoginInfo);
            _context.SaveChanges();

            return true;
        }

        public List<Organisation_Info> GetOrganisation_Infos()
        {
            var data = _context.Organisation_Info.ToList();

            return data;
        }
    }
}
