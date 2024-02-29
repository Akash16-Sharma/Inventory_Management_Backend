using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _customer;
        private readonly IRoles _Roles;
        public CustomerController(ICustomer customer, IRoles roles)
        {
            _customer = customer;
            _Roles = roles;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get(int OrgId)
        {
            var customer = _customer.GetCustomer(OrgId);
            if (customer == null)
            {
                return NotFound("No customer found for the specified organization.");
            }
            else
            {
                return Ok(customer);
            }
        }
    
        

        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddVendor([FromBody] Customer Cust, int StaffId)
        {
            Cust.Updated_By = StaffId;
            var checkRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (checkRoleTypeData.RoleType == "Admin")
            {
                bool isSave = _customer.AddCustomer(Cust);
                if (isSave)
                {
                    return Ok("Customer added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add Customer.");
                }
            }
            else
            {
                var accessData = _Roles.CheckAccess(StaffId);
                for (var i = 0; i < accessData.Count; i++)
                {
                    if (accessData[i].SideBarName == "Customer" && accessData[i].IsCreate == true)
                    {
                        bool isSave = _customer.AddCustomer(Cust);
                        if (isSave)
                        {
                            return Ok("Customer added successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to add Customer.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer([FromBody] Customer Cust, int StaffId)
        {
            Cust.Updated_By= StaffId;
            var checkRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (checkRoleTypeData.RoleType == "Admin")
            {
                bool isUpdate = _customer.UpdateCustomer(Cust);
                if (isUpdate)
                {
                    return Ok("Customer updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update Customer.");
                }
            }
            else
            {
                var accessData = _Roles.CheckAccess(StaffId);
                for (var i = 0; i < accessData.Count; i++)
                {
                    if (accessData[i].SideBarName == "Customer" && accessData[i].IsModify == true)
                    {
                        bool isUpdate = _customer.UpdateCustomer(Cust);
                        if (isUpdate)
                        {
                            return Ok("Customer updated successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to update Customer.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpDelete]
        [Route("DeleteCustomer")]
        public IActionResult DeleteCustomer(int id, int StaffId)
        {
            var checkRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (checkRoleTypeData.RoleType == "Admin")
            {
                bool isDelete = _customer.DeleteCustomer(id, StaffId);
                if (isDelete)
                {
                    return Ok("Customer deleted successfully.");
                }
                else
                {
                    return BadRequest("Failed to delete Customer.");
                }
            }
            else
            {
                var accessData = _Roles.CheckAccess(StaffId);
                for (var i = 0; i < accessData.Count; i++)
                {
                    if (accessData[i].SideBarName == "Customer" && accessData[i].IsModify == true)
                    {
                        bool isDelete = _customer.DeleteCustomer(id, StaffId);
                        if (isDelete)
                        {
                            return Ok("Customer deleted successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to delete Customer.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpPost]
        [Route("GetCustomerById")]
        public IActionResult GetCustomerById(int id, int StaffId)
        {
            var checkRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (checkRoleTypeData.RoleType == "Admin")
            {
                var data = _customer.GetCustomerById(id);
                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(data);
                }
            }
            else
            {
                var accessData = _Roles.CheckAccess(StaffId);
                for (var i = 0; i < accessData.Count; i++)
                {
                    if (accessData[i].SideBarName == "Customer" && accessData[i].IsModify == true)
                    {
                        var data = _customer.GetCustomerById(id);
                        if (data == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            return Ok(data);
                        }
                    }
                }
            }
            return BadRequest();
        }
    }
}
