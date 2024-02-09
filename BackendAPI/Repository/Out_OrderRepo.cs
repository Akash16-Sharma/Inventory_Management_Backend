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

        public bool DeleteOrder(int Id, int StaffId)
        {
            var data=_context.Out_Order.Where(x=>x.Id==Id).FirstOrDefault();
            if(data!=null)
            {
                data.IsActive = false;
                data.Updated_By = StaffId;
                _context.Out_Order.Update(data);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Out_Order GetOrderById(int id)
        {
            var data = _context.Out_Order.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

        public List<object> GetOutOrders(int OrgId)
        {
            var data = (from outOrder in _context.Out_Order
                        join customer in _context.Customer on outOrder.Customer_Id equals customer.Id
                        join item in _context.Items on outOrder.Item_Id equals item.Id
                        where outOrder.OrgId == OrgId && outOrder.IsActive == true
                        select new
                        {
                            outOrder.Id,
                            outOrder.Quantity,
                            outOrder.Actual_Date,
                            outOrder.Expected_Date,
                            CustomerName = customer.Name,
                            ItemName = item.Name,
                            ItemSellingPrice = item.Selling_Price,
                            // Add more properties as needed
                        }).ToList<object>();

            return data;
        }

        public bool UpdateOrder(Out_Order order)
        {
            var data = _context.Out_Order.Where(x => x.Id == order.Id).FirstOrDefault(); 
            if(data!=null)
            {
                data.Customer_Id = order.Customer_Id;
                data.Updated_By= order.Updated_By;
                data.Item_Id= order.Item_Id;
                data.Expected_Date = order.Expected_Date;
                data.Actual_Date = order.Actual_Date;
                data.Quantity = order.Quantity;
                _context.Out_Order.Update(data);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
