using BackendAPI.IRepository;
using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public class DashboardRepo:IDashboard
    {
        private readonly DataContext _Context;
        public DashboardRepo(DataContext context)
        {
            _Context = context;
        }

        public List<Inc_Order> HighestPurchasingItem(int orgid)
        {
            var data = _Context.Inc_Order
                     .Where(x => x.OrgId == orgid && x.IsActive == true)
                     .GroupBy(x => x.Item_Id)
                     .Where(group => group.Count() >= 5)  // Filter groups with count greater than or equal to 5
                     .Select(group => group.First())
                     .ToList();

            return data;

        }


        public List<Out_Order> HighestSellingItem(int orgid)
        {

            var data = _Context.Out_Order
                     .Where(x => x.OrgId == orgid && x.IsActive == true)
                     .GroupBy(x => x.Item_Id)
                     .Where(group => group.Count() >= 5)  // Filter groups with count greater than or equal to 5
                     .Select(group => group.First())
                     .ToList();

            return data;

        }

        public List<Item> LowStockItem(int orgid)
        {
            var data = _Context.Items
                            .Where(x => x.Org_Id == orgid && x.IsActive == true && x.Stock_Alert > x.Opening_Stock)
                            .Select(item => new Item
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Opening_Stock = item.Opening_Stock,
                                // Add more properties as needed
                            })
                            .ToList();

            return data;
        }



        public List<Inc_Order> TotalIncOrder(int orgid)
        {
            var data = _Context.Inc_Order
                    .Where(x => x.OrgId == orgid && x.IsActive == true)
                    .GroupBy(x => x.Purchase_Order_Id)
                    .Select(group => group.First())
                    .ToList();


            return data;
        }


        public List<Item> TotalItems(int orgid)
        {
            var data=_Context.Items.Where(x=>x.Org_Id== orgid && x.IsActive == true).ToList();
            return data;
        }

        public List<Out_Order> TotalOut_Order(int orgid)
        {
            var data = _Context.Out_Order
                                .Where(x => x.OrgId == orgid && x.IsActive == true)
                                .GroupBy(x => x.Sales_Order_Id)
                                .Select(group => group.First())
                                .ToList();

            return data;
        }
    }
}
