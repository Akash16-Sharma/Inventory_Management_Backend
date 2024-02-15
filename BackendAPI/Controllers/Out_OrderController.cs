using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Out_OrderController : ControllerBase
    {
        private readonly IOut_Order _OutOrder;
        private readonly IRoles _roles;
        public Out_OrderController(IOut_Order outOrder,IRoles roles)
        {
            _OutOrder = outOrder;
            _roles = roles;
        }

        [HttpGet]
        [Route("GetOrderInfo")]
        public IActionResult GetOrderInfo(int orgid, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var data = _OutOrder.GetOutOrderInfo(orgid);
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
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsShow == true)
                    {
                        var data = _OutOrder.GetOutOrderInfo(orgid);
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

        [HttpGet]
        [Route("GetOrderInfoBySalesOrderId")]
        public IActionResult GetOrderInfoBySalesOrderId(string SalesOrderID, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var data = _OutOrder.GetOutOrdersBySalesOrderID(SalesOrderID);
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
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsShow == true)
                    {
                        var data = _OutOrder.GetOutOrdersBySalesOrderID(SalesOrderID);
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
        public IActionResult AddOrder([FromBody] Out_OrderRequest order, int StaffId)
        {
            order.order.Updated_By = StaffId;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                for (var i = 0; i < order.OrderItems.Count; i++)
                {
                    order.order.Item_Id = order.OrderItems[i].Item_Id;
                    order.order.Quantity= order.OrderItems[i].Quantity;
                    bool IsAdd = _OutOrder.AddOrder(order.order);
                    if (IsAdd&&i==order.OrderItems.Count)
                    {
                        return Ok("Order added successfully.");
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsCreate == true)
                    {
                        for (var a = 0; a < order.OrderItems.Count; a++)
                        {
                            order.order.Item_Id = order.OrderItems[a].Item_Id;
                            order.order.Quantity = order.OrderItems[a].Quantity;
                            bool IsAdd = _OutOrder.AddOrder(order.order);
                            if (IsAdd&&a==order.OrderItems.Count)
                            {
                                return Ok("Order added successfully.");
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] Out_OrderRequest order, int StaffId,string SellOrderId)
        {
            int Count = 0;
            order.order.Updated_By = StaffId;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                for (var i = 0; i < order.OrderItems.Count; i++)
                {
                    order.order.Item_Id = order.OrderItems[i].Item_Id;
                    order.order.Quantity = order.OrderItems[i].Quantity;
                    bool IsUpdate = _OutOrder.UpdateOrder(order.order,SellOrderId,Count);
                    if (IsUpdate&&i==order.OrderItems.Count)
                    {
                        return Ok("Order updated successfully.");
                    }
                    else
                    {
                        Count++;
                        continue;
                    }
                }
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsModify == true)
                    {
                        for (var j = 0; j < order.OrderItems.Count; j++)
                        {
                            order.order.Item_Id = order.OrderItems[j].Item_Id;
                            order.order.Quantity = order.OrderItems[j].Quantity;
                            bool IsUpdate = _OutOrder.UpdateOrder(order.order,SellOrderId,Count);
                            if (IsUpdate&&j==order.OrderItems.Count)
                            {
                                return Ok("Order updated successfully.");
                            }
                            else
                            {
                                Count++;
                                continue;
                            }
                        }
                        return Ok("Order updated successfully.");
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(string SellOrderId, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsDelete = _OutOrder.DeleteOrder(SellOrderId, StaffId);
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
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsModify == true)
                    {
                        bool IsDelete = _OutOrder.DeleteOrder(SellOrderId, StaffId);
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
                var data = _OutOrder.GetOrderById(id);
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
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsShow == true)
                    {
                        var data = _OutOrder.GetOrderById(id);
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
    

