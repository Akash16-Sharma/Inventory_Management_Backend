using BackendAPI.IRepository;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class City_StateRepo:ICity_State
    {
        private readonly DataContext _CityState;
        public City_StateRepo(DataContext cityState)
        {
            _CityState = cityState;
        }

        public List<Cities_List> GetAllCities(int Stateid)
        {
            var CityData=_CityState.Cities_List.Where(x=>x.StateID==Stateid).ToList();
            return CityData;
        }

        public List<StateList> GetAllStates()
        {
            var AllStatesData=_CityState.State_List.ToList();
            return AllStatesData;
        }
    }
}
