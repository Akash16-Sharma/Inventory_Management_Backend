using BackendAPI.IRepository;
using BackendAPI.Models;


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

        public bool DeleteItem(Item Items)
        {
          var del= _Item.Items.Where(x => x.Id == Items.Id).FirstOrDefault();
            del.IsActive=false;
            _Item.Items.Update(del);
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
            //var itemData=_Item.Items.Where(x => x.Id==Items.Id).FirstOrDefault();
            //itemData = Items;
           _Item.Items.Update(Items);
           int IsUpdate= _Item.SaveChanges();
            if (IsUpdate==1 )
            {
                return true;
            }
            else { return false; }
        }

        public Item GetItemById(int id)
        {
            var ItemData=_Item.Items.Where(x=>x.Id==id).FirstOrDefault(); 
            return ItemData;
        }

        public List<object> GetItemInfo(int orgid)
        {
            var data = (from category in _Item.Category
                        join item in _Item.Items on category.Id equals item.Category_Id
                        join vendor in _Item.Vendor on item.Vendor_Id equals vendor.Id
                        join unitType in _Item.Unit_Type on item.Unit_Type_Id equals unitType.Id
                        where item.Org_Id == orgid && item.IsActive == true
                        select new
                        {
                            item.Name,
                            item.Buying_Price,
                            item.Selling_Price,
                            item.Id,
                            CategoryName = category.Name,  
                            VendorName = vendor.Name,      
                            UnitTypeName = unitType.Name,  
                        }).ToList<object>();

            return data;
        }

    }
}
