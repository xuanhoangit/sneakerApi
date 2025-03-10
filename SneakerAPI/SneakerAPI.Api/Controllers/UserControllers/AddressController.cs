
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Api.Controllers;
using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Models.UserEntities;

namespace SneakerAPI.AdminApi.Controllers.ProductControllers
{   
    [Area("Client")]
    [Route("[Area]/api/[controller]")]
    [ApiController]
    [Authorize(Roles = RolesName.Customer)]
    public class AddressController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public AddressController(IUnitOfWork uow):base(uow)
        {
            _uow = uow;
        }
        
        [HttpGet("addresses/{infoId}")]
        public async Task<IActionResult> GetAddresses(int infoId)
        {   
            try
            {
                var addresses = await _uow.Address.GetAllAsync(x=>x.Address__CustomerInfo == infoId);
                if(addresses == null)
                {
                    return NotFound("Addresses not found");
                }
                return Ok(addresses);
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error occurred while getting addresses");
            }
        }
        
        [HttpGet("address/{id}")]
        public IActionResult GetAddress(int id)
        {
            try
            {
                
         
                var address = _uow.Address.Get(id);
                if(address == null)
                {
                    return NotFound("Address not found");
                }
                return Ok(address);
               }
            catch (System.Exception)
            {
                
                throw new Exception("An error occurred while getting address");
            }    
        }
        
        [HttpPost("new-address")]
        public IActionResult CreateAddress(Address address)
        {
            try
            {   
                var addressDefault= _uow.Address.GetFirstOrDefault(x=>x.Address__IsDefault == true && x.Address__CustomerInfo == address.Address__CustomerInfo);
                if(addressDefault == null)
                    address.Address__IsDefault = true;
                address.Address__IsDefault = false;
                _uow.Address.Add(address);
                _uow.Save();
                return Ok(address);
             }
            catch (System.Exception)
            {
                
                throw new Exception("An error occurred while creating address");
            }
        }
        
        [HttpPatch("update-address")]
        public IActionResult UpdateAddress(Address address)
        {
            try
            {
                _uow.Address.Update(address);
                _uow.Save();
                return Ok(address);
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error occurred while updating address");
            }
        }
        
        [HttpDelete("delete-address/{id}")]
        public IActionResult DeleteAddress(int id)
        {
            try
            {
                var address = _uow.Address.Get(id);
                if(address == null)
                {
                    return NotFound("Address not found");
                }
                _uow.Address.Remove(address);
                _uow.Save();
                return Ok("Address deleted");
            }
            catch (System.Exception)
            {
                
                throw new Exception("An error occurred while deleting address");
            }
        }
    }
}