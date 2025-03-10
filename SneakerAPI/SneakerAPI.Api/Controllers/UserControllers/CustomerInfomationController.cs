using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Models.UserEntities;

namespace SneakerAPI.Api.Controllers.UserControllers
{   
    [Area("Client")]
    [Route("[Area]/api/[controller]")]
    [ApiController]
    [Authorize(Roles = RolesName.Customer)]
    public class UserInfomationController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        // private int _userId;
        
        public UserInfomationController(IUnitOfWork uow, IHttpContextAccessor httpContextAccessor) : base(uow)
        {
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return int.Parse(user?.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet("profile")]
        
        public IActionResult GetCustomerInfomation()
        {
            try
            {
                var customer = _uow.CustomerInfo.Get(GetUserId());
                if(customer == null)
                {
                    return NotFound("User profile not found");
                }
                return Ok(customer);
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error occurred while getting user profile");
            }
        }
        [HttpPatch("profile-update")]
        
        public IActionResult UpdateCustomerInfomation(CustomerInfo customerInfo)
        {
           try
           {
                var _customerInfo = _uow.CustomerInfo.Get(GetUserId());
                if(_customerInfo == null)
                {
                    return NotFound("User profile not found");
                }
                _customerInfo.CustomerInfo__FirstName = customerInfo.CustomerInfo__FirstName;
                _customerInfo.CustomerInfo__LastName = customerInfo.CustomerInfo__LastName;
                _customerInfo.CustomerInfo__SpendingPoint = customerInfo.CustomerInfo__SpendingPoint;
                _customerInfo.CustomerInfo__TotalSpent = customerInfo.CustomerInfo__TotalSpent;
                _uow.CustomerInfo.Update(_customerInfo);
                _uow.Save();
                return Ok(_customerInfo);
           }
           catch (System.Exception)
           {
            
            throw new Exception("An error occurred while updating user profile");
           }
        }

    }
}
