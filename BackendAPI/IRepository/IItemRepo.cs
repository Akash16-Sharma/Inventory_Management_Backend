using INVT_MNGMNT.Model.DataModels;

namespace BackendAPI.IRepository
{
    public interface IItemRepo
    {
        bool AddItem(Item Items);
        bool UpdateItem(Item Items);
        bool DeleteItem(int id);
    }
}
