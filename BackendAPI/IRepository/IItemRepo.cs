

using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IItemRepo
    {
        bool AddItem(Item Items);
        bool UpdateItem(Item Items);
        bool DeleteItem(int id);
    }
}
