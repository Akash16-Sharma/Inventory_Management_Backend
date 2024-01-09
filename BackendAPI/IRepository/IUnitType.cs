using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IUnitType
    {
        List <UnitType> GetAllUnitType();
        bool AddUnitType(UnitType unitType);
        bool DeleteUnitType(int id);
        bool UpdateUnitType(UnitType unitType);
    }
}
