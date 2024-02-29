using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models;
using BackendAPI.Models.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Inc_OrderController : ControllerBase
    {
        private readonly IInc_Orders _incord;
        private readonly IRoles _roles;

        public Inc_OrderController(IInc_Orders incord, IRoles Roles)
        {
            _incord = incord;
            _roles = Roles;
        }

        [HttpGet]
        [Route("GetOrderInfo")]
        public IActionResult GetOrderInfo(int orgid)
        {
           
                var data = _incord.GetOrderInfo(orgid);
                if (data == null)
                {
                    return NotFound("No order information found.");
                }
                else
                {
                    return Ok(data);
                }
           
        }

        [HttpGet]
        [Route("GetOrderInfoByPurchaseOrderId")]
        public IActionResult GetOrderInfoByPurchaseOrderId(string PurchaseOrderId, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var data = _incord.GetOrderInfoByPurchase(PurchaseOrderId);
                if (data == null)
                {
                    return NotFound("No order information found.");
                }
                else
                {
                    return Ok(data);
                }
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Incoming Orders" && Accessdata[i].IsShow == true)
                    {
                        var data = _incord.GetOrderInfoByPurchase(PurchaseOrderId);
                        if (data == null)
                        {
                            return NotFound("No order information found.");
                        }
                        else
                        {
                            return Ok(data);
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder([FromBody]IncOrderRequest ord, int StaffId)
        {
            ord.Inc_Orders.Updated_By = StaffId;
            //ord.Inc_Orders.Order_Date = DateOnly.Parse(ord.OrderDate);
            //ord.Inc_Orders.Expected_Date=DateOnly.Parse(ord.ExpectedDate);
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                for (var i = 0; i < ord.OrderItems.Count; i++)
                {
                    ord.Inc_Orders.Item_Id = ord.OrderItems[i].Item_Id;
                    ord.Inc_Orders.Quantity= ord.OrderItems[i].Quantity;
                    bool IsAdd = _incord.AddOrder(ord.Inc_Orders); 
                    if (IsAdd&&i==ord.OrderItems.Count)
                    {
                        return Ok("Order added successfully.");
                    }
                    else
                    {
                        continue;
                    }
                }
                return Ok("Order added successfully.");
            }
            else
            {
                var a = 0;
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Incoming Orders" && Accessdata[i].IsCreate == true)
                    {
                        for ( a = 0; a < ord.OrderItems.Count; a++)
                        {
                            ord.Inc_Orders.Item_Id = ord.OrderItems[a].Item_Id;
                            ord.Inc_Orders.Quantity = ord.OrderItems[a].Quantity;
                            bool IsAdd = _incord.AddOrder(ord.Inc_Orders);
                            continue;
                        }
                        if ( a>0)
                        {
                            return Ok("Order added successfully.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] IncOrderRequest ord, int StaffId)
        {
            ord.Inc_Orders.Updated_By = StaffId;
            //ord.Inc_Orders.Order_Date = DateOnly.Parse(ord.OrderDate);
            //ord.Inc_Orders.Expected_Date = DateOnly.Parse(ord.ExpectedDate);//Converting String To DateOnly
            int count = 0;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                for (var i = 0; i < ord.OrderItems.Count; i++)
                {
                    ord.Inc_Orders.Item_Id = ord.OrderItems[i].Item_Id;
                    ord.Inc_Orders.Quantity = ord.OrderItems[i].Quantity;
                    
                    _incord.UpdateOrder(ord.Inc_Orders, ord.Inc_Orders.Purchase_Order_Id,count);
                        count++;
                        continue;
                }
                return Ok("Order updated successfully.");
            }
            else
            {
                var a = 0;
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Incoming Orders" && Accessdata[i].IsModify == true)
                    {
                        for ( a = 0; a < ord.OrderItems.Count; a++)
                        {
                            ord.Inc_Orders.Item_Id = ord.OrderItems[a].Item_Id;
                            ord.Inc_Orders.Quantity = ord.OrderItems[a].Quantity;
                            bool IsUpdate = _incord.UpdateOrder(ord.Inc_Orders, ord.Inc_Orders.Purchase_Order_Id, count);
                                count++;
                                continue;
                            
                        }
                        if(a>0)
                        {
                            return Ok("Order updated successfully.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(string PurchaseOrderId, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsDelete = _incord.DeleteOrder(PurchaseOrderId, StaffId);
                if (IsDelete)
                {
                    return Ok("Order deleted successfully.");
                }
                else
                {
                    return BadRequest("Failed to delete order.");
                }
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Incoming Orders" && Accessdata[i].IsModify == true)
                    {
                        bool IsDelete = _incord.DeleteOrder(PurchaseOrderId, StaffId);
                        if (IsDelete)
                        {
                            return Ok("Order deleted successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to delete order.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpGet]
        [Route("GetOrderById")]
        public IActionResult GetOrder(int id, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var data = _incord.GetOrderInfoById(id);
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest("No order information found for the given ID.");
                }
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Incoming Orders" && Accessdata[i].IsShow == true)
                    {
                        var data = _incord.GetOrderInfoById(id);
                        if (data != null)
                        {
                            return Ok(data);
                        }
                        else
                        {
                            return BadRequest("No order information found for the given ID.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }
    }
}
