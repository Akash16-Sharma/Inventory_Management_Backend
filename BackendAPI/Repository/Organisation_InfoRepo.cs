using BackendAPI.AuthServices;
using BackendAPI.IRepository;
using BackendAPI.Models;
using BackendAPI.Models.Class;
using BackendAPI.Models.Roles;
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
            info.Inserted_On = DateTime.Now;

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
                InsertedOn = info.Inserted_On,
            };

            _context.Organisation_Info.Add(orgInfo);
            _context.SaveChanges();

            // Creating User Login Info
            string PassSecret = "b14ca5898a4e4133bbce2ea2315a1916";
            
            
               Staff staff= new Staff();
                staff.IsActive = true;
                staff.Staff_Name = info.Name;
                staff.Email = info.Org_Email;
                staff.Password = _encDycPassword.EncryptPassword(PassSecret, info.Password);
                staff.OrgId = orgInfo.Id;
                staff.InsertedOn = DateTime.Now;
                staff.RoleType = "Admin";
                _context.Staff.Add(staff);
                _context.SaveChanges();
                return true;


        }

        public List<Organisation_Info> GetOrganisation_Infos(int OrgId)
        {
            var data = _context.Organisation_Info.Where(x=>x.Id==OrgId&&x.IsActive==true).ToList();
            return data;
        }

        public Organisation_Info GetOrgById(int OrgId)
        {
            var data=_context.Organisation_Info.Where(x=>x.Id==OrgId).FirstOrDefault();
            return data;
        }
    }
}
