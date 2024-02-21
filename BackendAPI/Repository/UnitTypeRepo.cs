using BackendAPI.IRepository;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class UnitTypeRepo:IUnitType
    {
        private readonly DataContext _UnitType;

        public UnitTypeRepo(DataContext unitType)
        {
            _UnitType = unitType;
        }

        //public bool AddUnitType(UnitType unitType)
        //{
        //    unitType.InsertedOn=DateTime.Now;
        //    var UnitTypeData=_UnitType.Unit_Type.Where(x=>x.Name == unitType.Name).FirstOrDefault();
        //    if (UnitTypeData != null)
        //    {
        //        UnitTypeData.IsActive = true;
        //        UnitTypeData.InsertedOn= DateTime.Now;
        //        _UnitType.Update(UnitTypeData);
        //        _UnitType.SaveChanges();
        //        return true;
        //    }
        //    else
        //    {
        //        unitType.IsActive = true;
        //        _UnitType.Unit_Type.Add(unitType);
        //       int IsSave= _UnitType.SaveChanges();
        //        if (IsSave > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        //public bool DeleteUnitType(int id, int StaffId)
        //{
        //    var DeleteData=_UnitType.Unit_Type.Where(x=>x.Id==id).FirstOrDefault();
        //    if (DeleteData != null)
        //    {
        //        DeleteData.UpdatedBy = StaffId;
        //        DeleteData.IsActive = false;
        //        DeleteData.InsertedOn= DateTime.Now;
        //        _UnitType.Unit_Type.Update(DeleteData);
        //        _UnitType.SaveChanges();
        //        return true;
        //    }
        //    else { return false; }
        //}

        public List <UnitType> GetAllUnitType(int OrgId)
        {
            var UnitTypeData = _UnitType.Unit_Type.ToList(); //get all data
            return UnitTypeData;
        }
            
        //public UnitType GetUnitTypeById(int id)
        //{
        //   var data=_UnitType.Unit_Type.Where(x=>x.Id== id).FirstOrDefault();
        //    return data;
        //}

        //public bool UpdateUnitType(UnitType unitType)
        //{
        //    unitType.InsertedOn=DateTime.Now;
        //    var unittypedata=_UnitType.Unit_Type.Where(x=>x.Id==unitType.Id).FirstOrDefault();
        //    unittypedata.Name=unitType.Name;
        //    unittypedata.UpdatedBy=unitType.UpdatedBy;
        //    _UnitType.Unit_Type.Update(unittypedata);
        //   int IsUpdated= _UnitType.SaveChanges();
        //    if(IsUpdated > 0)
        //    {
        //        return true;
        //    }
        //    else
        //        return false;

        //}
    }
}
