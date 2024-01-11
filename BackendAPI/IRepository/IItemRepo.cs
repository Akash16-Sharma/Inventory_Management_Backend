

using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface IItemRepo
    {

        Item GetItemById(int id);
        bool AddItem(Item Items);
        bool UpdateItem(Item Items);
        bool DeleteItem(int id);
    }
}
