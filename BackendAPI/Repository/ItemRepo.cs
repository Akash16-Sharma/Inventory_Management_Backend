using BackendAPI.IRepository;
using BackendAPI.Models;
using INVT_MNGMNT.Model.DataModels;

namespace BackendAPI.Repository
{
    public class ItemRepo:IItemRepo
    {
        private readonly DataContext _Item;

        public ItemRepo(DataContext item)
        {
            _Item = item;
        }

        public bool AddItem(Item Items)
        {
            Items.InsertedOn = DateTime.Now;
            //var IsAdded = _Item.Items.Where(x => x.Org_Id == Items.Org_Id).FirstOrDefault();
            //if (IsAdded != null)
            //{
            //    IsAdded.IsActive = true;
            //    _Item.Items.Add(Items);
            //    _Item.SaveChanges();
            //    return true;
            //}
           
                Items.IsActive = true;
                _Item.Items.Add(Items);
                int IsSave = _Item.SaveChanges();
                if (IsSave == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
        }

        public bool DeleteItem(int id)
        {
          var del= _Item.Items.Where(x => x.Id == id).FirstOrDefault();
            del.IsActive=false;
            _Item.Items.Add(del);
            int IsDelete= _Item.SaveChanges();
            if(IsDelete==1 )
            {
                return true;
            }
            else
                return false;
        }

        public bool UpdateItem(Item Items)
        {
           _Item.Items.Update(Items);
           int IsUpdate= _Item.SaveChanges();
            if (IsUpdate==1 )
            {
                return true;
            }
            else { return false; }
        }
    }
}
