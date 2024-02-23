using BackendAPI.IRepository;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class Inc_OrderRepo:IInc_Orders
    {
        private readonly DataContext _Context;
        public Inc_OrderRepo(DataContext context)
        {
            _Context = context;
        }

        public bool AddOrder(Inc_Order order)
        {
            order.Id = 0;

          
            order.Inserted_On = DateTime.Now;
           // order.Actual_Date = DateTime.Now; //testing purpose
           
            order.IsActive = true;
            _Context.Inc_Order.Add(order);
           int i= _Context.SaveChanges();
            if(i>0)
            {
                return true;
            }
            return false;
        }
        // var item_Data = _Context.Items.Where(x => x.Id == order.Item_Id).FirstOrDefault();

        //order.PurchaseAmount = item_Data.Buying_Price * order.Quantity;
        // item_Data.Id = 0;
        public bool DeleteOrder(string purchaseorderid,int StaffId)
        {
            var i = 0;
            var data=_Context.Inc_Order.Where(x=>x.Purchase_Order_Id==purchaseorderid).ToList();
            if (data != null)
            {
                for (i = 0; i < data.Count; i++)
                {
                    data[i].Inserted_On = DateTime.Now;
                    data[i].IsActive = false;
                    data[i].Updated_By = StaffId;
                    _Context.Inc_Order.Update(data[i]);
                    _Context.SaveChanges();
                    
                }
                return true;
            }
            return false;
        }



        public List<object> GetOrderInfoByPurchase(string Purchase_Order_Id)
        {
            var data = (from incOrder in _Context.Inc_Order
                        join vendor in _Context.Vendor on incOrder.Vendor_Id equals vendor.Id
                        join item in _Context.Items on incOrder.Item_Id equals item.Id
                        where incOrder.Purchase_Order_Id == Purchase_Order_Id&&incOrder.IsActive==true
                        select new
                        {
                            incOrder.Id,
                            incOrder.Purchase_Order_Id, //added by prateek
                            incOrder.Vendor_Id,
                            incOrder.Quantity,
                            incOrder.Actual_Date,
                           // incOrder.Expected_Date,
                            incOrder.Order_Date,
                            VendorName = vendor.Name,
                            ItemName = item.Name,
                            ItemId =item.Id,
                            ItemBuyingPrice = item.Buying_Price,
                            // Add more properties as needed
                        }).ToList<object>();

            return data;
        }

        public List<object> GetOrderInfo(int orgid)
        {
            var data = (from incOrder in _Context.Inc_Order
                        join vendor in _Context.Vendor on incOrder.Vendor_Id equals vendor.Id
                        join item in _Context.Items on incOrder.Item_Id equals item.Id
                        where incOrder.OrgId == orgid && incOrder.IsActive == true
                        select new
                        {
                            incOrder.Id,
                            incOrder.Purchase_Order_Id,
                            incOrder.Order_Date,
                            VendorName = vendor.Name,
                        })
                        .GroupBy(x => x.Purchase_Order_Id) // Group by Purchase Order Id
                        .Select(group => group.First())    // Select the first item from each group
                        .ToList<object>();

            return data;
        }


        public Inc_Order GetOrderInfoById(int id)
        {
            var data = _Context.Inc_Order.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

       

        public bool UpdateOrder(Inc_Order order,string PurchaseOrderId,int count)
        {
            var i = count;
            var Incdata = _Context.Inc_Order.Where(x => x.Purchase_Order_Id == PurchaseOrderId && x.IsActive == true).ToList();
            
            if(Incdata != null)
            {
                while (i < Incdata.Count)
                {
                    order.Id = 0;
                    order.Id= Incdata[i].Id;
                    var data = _Context.Inc_Order.Where(x => x.Id == order.Id).FirstOrDefault();
                    data.Inserted_On = DateTime.Now;
                    data.Actual_Date = order.Actual_Date;
                    data.IsActive = true;
                    data.Updated_By = order.Updated_By;
                    data.Vendor_Id = order.Vendor_Id;
                    data.Item_Id = order.Item_Id;
                    data.Quantity = order.Quantity;
                   // data.Expected_Date = order.Expected_Date;
                    data.Order_Date = order.Order_Date;
                    _Context.Inc_Order.Update(data);
                    _Context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
