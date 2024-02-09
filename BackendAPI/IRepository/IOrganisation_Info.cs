using BackendAPI.Models;
//using DocumentFormat.OpenXml.Spreadsheet;

namespace BackendAPI.IRepository
{
    public interface IOrganisation_Info
    {
         bool AddOrganisation_Info(AddAllInfo Info);

        List<Organisation_Info> GetOrganisation_Infos(int OrgId);
        Organisation_Info GetOrgByID(int OrgId);
        bool Update_Organisation(Organisation_Info Info);


        //Adding function for Userinfo 
        bool AddUserInfo(User_Info info);
        bool UpdateUserInfo(User_Info info);
        User_Info GetUserInfoByid(int orgid);
        User_Info UserInfo(int orgid);
    }
}
