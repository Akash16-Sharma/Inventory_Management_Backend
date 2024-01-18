using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IUnitType
    {
        List <UnitType> GetAllUnitType(int OrgId);
        bool AddUnitType(UnitType unitType);
        bool DeleteUnitType(UnitType unit);
        bool UpdateUnitType(UnitType unitType);
    }
}
