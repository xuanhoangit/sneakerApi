using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakerAPI.AdminApi.Controllers;
using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Models.UserEntities;

namespace SneakerAPI.Api.Controllers.UserControllers
{   
    [Area("Dashboard")]
    [Route("[Area]/api/[controller]")]
    [ApiController]
    [Authorize(Roles = RolesName.Staff)]
    public class StaffInfomationController : BaseController
    {
        private readonly IUnitOfWork _uow;
        
        // private int _userId;
        
        public StaffInfomationController(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }


        [HttpGet("profile")]
        
        public IActionResult GetStaffInfomation(int staffInfo)
        {
            try
            {
                var staff = _uow.StaffInfo.Find(x=>x.StaffInfo__AccountId==staffInfo);
                if(staff == null)
                {
                    return NotFound("User profile not found");
                }
                return Ok(staff);
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error occurred while getting user profile");
            }
        }
        [HttpPatch("profile-update")]
        
        public IActionResult UpdateStaffInfomation(StaffInfo staffInfo)
        {
           try
           {
                var _staffInfo = _uow.StaffInfo.Get(staffInfo.StaffInfo__Id);
                if(_staffInfo == null)
                {
                    return NotFound("User profile not found");
                }
                _staffInfo.StaffInfo__FirstName = staffInfo.StaffInfo__FirstName;
                _staffInfo.StaffInfo__LastName = staffInfo.StaffInfo__LastName;
                _staffInfo.StaffInfo__AccountId=staffInfo.StaffInfo__AccountId;
                _staffInfo.StaffInfo__Avatar=staffInfo.StaffInfo__Avatar;
                _staffInfo.StaffInfo__Phone=staffInfo.StaffInfo__Phone;
                
                _uow.StaffInfo.Update(_staffInfo);
                _uow.Save();
                return Ok(_staffInfo);
           }
           catch (System.Exception)
           {
            
            throw new Exception("An error occurred while updating user profile");
           }
        }

    }
}
