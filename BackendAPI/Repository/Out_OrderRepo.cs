using BackendAPI.IRepository;
using BackendAPI.Models;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace BackendAPI.Repository
{
    public class Out_OrderRepo:IOut_Order
    {
        private readonly DataContext _context;
        public Out_OrderRepo(DataContext context)
        {
            _context = context;
        }

        public bool AddOrder(Out_Order order)
        {
            var ItemData=_context.Items.Where(x=>x.Id==order.Item_Id).FirstOrDefault();
            if(ItemData.Opening_Stock<order.Quantity)
            {
                return false;
            }
           else
            {
                order.Id = 0;
                ItemData.Opening_Stock=ItemData.Opening_Stock - order.Quantity;
                ItemData.Updated_By=order.Updated_By;
                ItemData.InsertedOn = DateTime.Now;
                _context.Items.Update(ItemData);
                _context.SaveChanges(); 
                order.Inserted_On = DateTime.Now;
                order.IsActive = true;
                _context.Out_Order.Add(order);
                _context.SaveChanges();
                int i = _context.SaveChanges();
                if (i > 0)
                {
                    return true;
                }
                else
                { return false; }
            }
        }

        public bool DeleteOrder(string SellOrderId, int StaffId)
        {
            var data=_context.Out_Order.Where(x=>x.Sales_Order_Id==SellOrderId).ToList();
            if(data!=null)
            {
                for (var i = 0; i < data.Count; i++)
                {
                    data[i].IsActive = false;
                    data[i].Updated_By = StaffId;
                    data[i].Inserted_On = DateTime.Now;
                    _context.Out_Order.Update(data[i]);
                    _context.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public Out_Order GetOrderById(int id)
        {
            var data = _context.Out_Order.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public List<object> GetOutOrderInfo(int orgid)
        {
            var data = (from outorder in _context.Out_Order
                        join customer in _context.Customer on outorder.Customer_Id equals customer.Id
                        join item in _context.Items on outorder.Item_Id equals item.Id
                        where outorder.OrgId == orgid && outorder.IsActive == true
                        select new
                        {
                            outorder.Id,
                            outorder.Sales_Order_Id,
                            outorder.Order_Date,
                            CustomerName = customer.Name,
                        })
                         .GroupBy(x => x.Sales_Order_Id) // Group by Sales Order Id
                         .Select(group => group.First())    // Select the first item from each group
                         .ToList<object>();

            return data;
        }

        public List<object> GetOutOrdersBySalesOrderID(string SalesOrderID)
        {
            var data = (from outOrder in _context.Out_Order
                        join customer in _context.Customer on outOrder.Customer_Id equals customer.Id
                        join item in _context.Items on outOrder.Item_Id equals item.Id
                        where outOrder.Sales_Order_Id == SalesOrderID && outOrder.IsActive == true
                        select new
                        {
                            outOrder.Id,
                            outOrder.Quantity,
                            outOrder.Actual_Date,
                            outOrder.Customer_Id,//returning customer id
                            //outOrder.Expected_Date,
                            CustomerName = customer.Name,
                            ItemName = item.Name,
                            ItemSellingPrice = item.Selling_Price,
                            // Add more properties as needed
                        }).ToList<object>();

            return data;
        }

        public bool UpdateOrder(Out_Order order, string SellOrderId, int Count)
        {
            var i = Count;
            //Updating The Opening Stock Of Item
            var ItemData = _context.Items.Where(x => x.Id == order.Item_Id).FirstOrDefault();
            if (ItemData.Opening_Stock < order.Quantity)
            {
                return false;
            }
            ItemData.Opening_Stock -= order.Quantity;
            ItemData.Updated_By = order.Updated_By;
            ItemData.InsertedOn = DateTime.Now;
            _context.Items.Update(ItemData);
            _context.SaveChanges();
            var OutDataList=_context.Out_Order.Where(x=>x.Sales_Order_Id==SellOrderId&&x.IsActive==true).ToList();
            
                while (i < OutDataList.Count)
            {
                order.Id = 0;
                order.Id = OutDataList[i].Id;
                var data = _context.Out_Order.Where(x => x.Id == order.Id).FirstOrDefault();
                if (data != null)
                {
                    data.Customer_Id = order.Customer_Id;
                    data.Updated_By = order.Updated_By;
                    data.Item_Id = order.Item_Id;
                   // data.Expected_Date = order.Expected_Date;
                    data.Actual_Date = order.Actual_Date;
                    data.Quantity = order.Quantity;
                    data.Inserted_On = DateTime.Now;
                    _context.Out_Order.Update(data);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
