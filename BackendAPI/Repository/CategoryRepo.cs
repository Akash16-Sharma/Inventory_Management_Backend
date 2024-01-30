using BackendAPI.IRepository;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class CategoryRepo:ICategory
    {
        private readonly DataContext _Category;
        public CategoryRepo(DataContext category)
        {
            _Category = category;
        }

        public bool AddCategory(Category category)
        {
            category.InsertedOn = DateTime.Now;
            category.IsActive= true;
            var CatData=_Category.Category.Where(x=>x.Name == category.Name&&x.OrgId==category.OrgId).FirstOrDefault();
            if (CatData != null)
            {
                CatData.IsActive = true;
                CatData.InsertedOn = DateTime.Now;
                _Category.Update(CatData);
                _Category.SaveChanges();
                return true;
            }
            else
            {
                _Category.Category.Add(category);
               int IsSave= _Category.SaveChanges();
                if (IsSave>0)
                {
                    return true;
                }
                else { return false; }

            }
        }

        public bool DeleteCategory(int id,int staffid)
        {
            var DeleteData=_Category.Category.Where(x=>x.Id==id).FirstOrDefault();
            if (DeleteData!= null)
            {
                DeleteData.UpdatedBy= staffid;
                DeleteData.IsActive = false;
                DeleteData.InsertedOn = DateTime.Now;
                _Category.Category.Update(DeleteData);
               _Category.SaveChanges();
                return true; 
            }
            else return false;
        }

        public List<Category> GetAllCategory(int OrgId)
        {
            var data=_Category.Category.Where(x=>x.OrgId==OrgId&&x.IsActive==true).ToList();
            return data;
        }

        public bool UpdateCategory(Category category)
        {
            category.InsertedOn= DateTime.Now;
            var UpdateCat=_Category.Category.Where(x=>x.Id==category.Id).FirstOrDefault();
            UpdateCat.Name=category.Name;
            UpdateCat.UpdatedBy= category.UpdatedBy;
            _Category.Category.Update(UpdateCat);
            _Category.SaveChanges();
            return true;
        }
    }
}
