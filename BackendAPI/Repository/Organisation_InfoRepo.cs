using BackendAPI.AuthServices;
using BackendAPI.IRepository;
using BackendAPI.Models;
using BackendAPI.Models.Class;
using BackendAPI.Models.Roles;
using DocumentFormat.OpenXml.Spreadsheet;
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
                PinCode = info.PinCode,
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

        public bool AddUserInfo(User_Info info)
        {
            if(info == null)
            {
                return false;
            }
            else
            {
                _context.User_Info.Add(info);
                _context.SaveChanges();
                return true;
            }
        }

        public User_Info UserInfo(int orgid)
        {
             var Data=_context.User_Info.Where(x=>x.OrgId == orgid).FirstOrDefault();
            return Data;
        }

        public List<Organisation_Info> GetOrganisation_Infos(int OrgId)
        {
            var data = _context.Organisation_Info.Where(x=>x.Id==OrgId&&x.IsActive==true).ToList();
            return data;
        }

        public Organisation_Info GetOrgByID(int OrgId)
        {
            var data=_context.Organisation_Info.Where(x=>x.Id== OrgId).FirstOrDefault();
            return data;
        }

        public User_Info GetUserInfoByid(int orgid)
        {
            var Data = _context.User_Info.Where(x => x.OrgId == orgid).FirstOrDefault();
            return Data;
        }

        public bool UpdateUserInfo(User_Info info)
        {
            var Data = _context.User_Info.Where(x => x.OrgId == info.OrgId).FirstOrDefault();
            if(Data != null)
            {
                Data.Name = info.Name;
                Data.Email = info.Email;
                Data.Phone = info.Phone;
                Data.IsActive = true;
                Data.InsertedOn=DateTime.Now;
                _context.User_Info.Update(Data);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool Update_Organisation(Organisation_Info Info)
        {
            var Exsistingdata = _context.Organisation_Info.Where(x => x.Id == Info.Id).FirstOrDefault();
            if (Exsistingdata != null)
            {
                Exsistingdata.Name = Info.Name;
                Exsistingdata.Email = Info.Email;
                Exsistingdata.CityID = Info.CityID;
                Exsistingdata.StateID = Info.StateID;
                Exsistingdata.Address = Info.Address;
                Exsistingdata.PinCode = Info.PinCode;
                Exsistingdata.PhoneNo = Info.PhoneNo;
                Exsistingdata.Type = Info.Type; //added by Prateek
                Exsistingdata.IsActive = true;
                Exsistingdata.InsertedOn = DateTime.Now;
                _context.Organisation_Info.Update(Exsistingdata);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
