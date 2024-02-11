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
        public IActionResult GetOrderInfo(int orgid, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
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
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsShow == true)
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
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder([FromBody]OrderRequest ord, int StaffId)
        {
            ord.Inc_Orders.Updated_By = StaffId;
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
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsCreate == true)
                    {
                        for (var a = 0; a < ord.OrderItems.Count; a++)
                        {
                            ord.Inc_Orders.Item_Id = ord.OrderItems[a].Item_Id;
                            ord.Inc_Orders.Quantity = ord.OrderItems[a].Quantity;
                            bool IsAdd = _incord.AddOrder(ord.Inc_Orders);
                            if (IsAdd&&a==ord.OrderItems.Count)
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
        public IActionResult UpdateOrder([FromBody] OrderRequest ord, int StaffId)
        {
            ord.Inc_Orders.Updated_By = StaffId;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                for (var i = 0; i < ord.OrderItems.Count; i++)
                {
                    ord.Inc_Orders.Item_Id = ord.OrderItems[i].Item_Id;
                    ord.Inc_Orders.Quantity = ord.OrderItems[i].Quantity;
                    bool IsUpdate = _incord.UpdateOrder(ord.Inc_Orders);
                    if (IsUpdate&&i==ord.OrderItems.Count)
                    {
                        return Ok("Order updated successfully.");
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
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsModify == true)
                    {
                        for (var a = 0; a < ord.OrderItems.Count; a++)
                        {
                            ord.Inc_Orders.Item_Id = ord.OrderItems[a].Item_Id;
                            ord.Inc_Orders.Quantity = ord.OrderItems[a].Quantity;
                            bool IsUpdate = _incord.UpdateOrder(ord.Inc_Orders);
                            if (IsUpdate&&a==ord.OrderItems.Count)
                            {
                                return Ok("Order updated successfully.");
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

        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(int id, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsDelete = _incord.DeleteOrder(id, StaffId);
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
                        bool IsDelete = _incord.DeleteOrder(id, StaffId);
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
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsShow == true)
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
