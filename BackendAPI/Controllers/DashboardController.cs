using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboard _dashboardRepository;
        private readonly IRoles _rolesRepository;

        public DashboardController(IDashboard dashboardRepository, IRoles rolesRepository)
        {
            _dashboardRepository = dashboardRepository;
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        [Route("TotalIncomingOrders")]
        public IActionResult TotalIncomingOrders(int orgId, int staffId)
        {
            var staffRole = _rolesRepository.CheckStaffType(staffId);
            if (staffRole.RoleType == "Admin")
            {
                var incomingOrders = _dashboardRepository.TotalIncOrder(orgId);
                if (incomingOrders != null)
                {
                    return Ok(incomingOrders.Count );
                }
                else
                {
                    return NotFound("No incoming orders found.");
                }
            }
            else
            {
                return BadRequest("Access denied. Insufficient privileges.");
            }
        }

        [HttpGet]
        [Route("TotalOutGoingOrder")]
        public IActionResult TotalOutGoingOrder(int orgId, int staffId)
        {
            var staffRole = _rolesRepository.CheckStaffType(staffId);
            if (staffRole.RoleType == "Admin")
            {
                var outgoingOrders = _dashboardRepository.TotalOut_Order(orgId);
                if (outgoingOrders != null)
                {
                    return Ok(new { TotalOutGoingOrder = outgoingOrders.Count });
                }
                else
                {
                    return NotFound("No outgoing orders found.");
                }
            }
            else
            {
                return BadRequest("Access denied. Insufficient privileges.");
            }
        }

        [HttpGet]
        [Route("TotalItems")]
        public IActionResult TotalItems(int orgId, int staffId)
        {
            var staffRole = _rolesRepository.CheckStaffType(staffId);
            if (staffRole.RoleType == "Admin")
            {
                var totalItems = _dashboardRepository.TotalItems(orgId);
                if (totalItems != null)
                {
                    return Ok(new { TotalItems = totalItems.Count });
                }
                else
                {
                    return NotFound("No items found.");
                }
            }
            else
            {
                return BadRequest("Access denied. Insufficient privileges.");
            }
        }

        [HttpGet]
        [Route("HighestPurchasingOrder")]
        public IActionResult HighestPurchasingOrder(int orgId, int staffId)
        {
            var staffRole = _rolesRepository.CheckStaffType(staffId);
            if (staffRole.RoleType == "Admin")
            {
                var highestPurchasingItems = _dashboardRepository.HighestPurchasingItem(orgId);
                if (highestPurchasingItems != null)
                {
                    return Ok(highestPurchasingItems);
                }
                else
                {
                    return NotFound("No highest purchasing items found.");
                }
            }
            else
            {
                return BadRequest("Access denied. Insufficient privileges.");
            }
        }

        [HttpGet]
        [Route("HighestSellingOrder")]
        public IActionResult HighestSellingOrder(int orgId, int staffId)
        {
            var staffRole = _rolesRepository.CheckStaffType(staffId);
            if (staffRole.RoleType == "Admin")
            {
                var highestSellingItems = _dashboardRepository.HighestSellingItem(orgId);
                if (highestSellingItems != null)
                {
                    return Ok(highestSellingItems);
                }
                else
                {
                    return NotFound("No highest selling items found.");
                }
            }
            else
            {
                return BadRequest("Access denied. Insufficient privileges.");
            }
        }

        [HttpGet]
        [Route("LowStock")]
        public IActionResult LowStock(int orgId, int staffId)
        {
            var staffRole = _rolesRepository.CheckStaffType(staffId);
            if (staffRole.RoleType == "Admin")
            {
                var lowStockItems = _dashboardRepository.LowStockItem(orgId);
                if (lowStockItems != null)
                {
                    return Ok(lowStockItems);
                }
                else
                {
                    return NotFound("No items with low stock found.");
                }
            }
            else
            {
                return BadRequest("Access denied. Insufficient privileges.");
            }
        }

        
    }
}
