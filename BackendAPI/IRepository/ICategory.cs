using BackendAPI.Models;

namespace BackendAPI.IRepository
{
    public interface ICategory
    {
       List <Category> GetAllCategory(int OrgId);
        bool AddCategory(Category category);
        bool DeleteCategory(int id,int staffid);
        bool UpdateCategory(Category category);
    }
}
