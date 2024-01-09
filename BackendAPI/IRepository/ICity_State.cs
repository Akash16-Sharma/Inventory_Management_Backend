using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface ICity_State
    {
       List <StateList> GetAllStates();
       List <Cities_List> GetAllCities(int Stateid);
    }
}
