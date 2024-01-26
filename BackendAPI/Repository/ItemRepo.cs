using BackendAPI.IRepository;
using BackendAPI.Models;


namespace BackendAPI.Repository
{
    public class ItemRepo:IItemRepo
    {
        private readonly DataContext _Item;

        public ItemRepo(DataContext Item)
        {
            _Item = Item;
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

        public bool UpdateItem(Item Item)
        {
            //var ItemData=_Item.Items.Where(x => x.Id==Items.Id).FirstOrDefault();
            //ItemData = Items;
            var existingItem = _Item.Items.Where(x => x.Id == Item.Id).FirstOrDefault();
            if (existingItem != null)
            {
                existingItem.Name = Item.Name;
                existingItem.Updated_By = Item.Updated_By;
                existingItem.Stock_Alert = Item.Stock_Alert;
                existingItem.Selling_Price = Item.Selling_Price;
                existingItem.Buying_Price = Item.Buying_Price;
                existingItem.Opening_Stock = Item.Opening_Stock;
                existingItem.Barcode = Item.Barcode;
                existingItem.Vendor_Id = Item.Vendor_Id;
                existingItem.Unit_Type_Id = Item.Unit_Type_Id;
                existingItem.Category_Id = Item.Category_Id;
                existingItem.InsertedOn = DateTime.Now;
                _Item.Items.Update(existingItem);
                int IsUpdate = _Item.SaveChanges();
                if (IsUpdate == 1)
                {
                    return true;
                }
                else { return false; }
            }
            return false;
        }

        public Item GetItemById(int id)
        {
            var ItemData=_Item.Items.Where(x=>x.Id==id).FirstOrDefault(); 
            return ItemData;
        }

        public List<object> GetItemInfo(int orgid)
        {
            var data = (from category in _Item.Category
                        join Item in _Item.Items on category.Id equals Item.Category_Id
                        join vendor in _Item.Vendor on Item.Vendor_Id equals vendor.Id
                        join unitType in _Item.Unit_Type on Item.Unit_Type_Id equals unitType.Id
                        where Item.Org_Id == orgid && Item.IsActive == true
                        select new
                        {
                            Item.Name,
                            Item.Buying_Price,
                            Item.Selling_Price,
                            Item.Id,
                            CategoryName = category.Name,  
                            VendorName = vendor.Name,      
                            UnitTypeName = unitType.Name,  
                        }).ToList<object>();

            return data;
        }

    }
}
