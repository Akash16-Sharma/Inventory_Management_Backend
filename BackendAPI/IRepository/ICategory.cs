using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface ICategory
    {
       List <Category> GetAllCategory();
        bool AddCategory(Category category);
        bool DeleteCategory(int id);
        bool UpdateCategory(Category category);
    }
}
