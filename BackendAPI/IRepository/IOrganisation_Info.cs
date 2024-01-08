using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IOrganisation_Info
    {
         bool AddOrganisation_Info(AddAllInfo Info);

        List<Organisation_Info> GetOrganisation_Infos();
    }
}
