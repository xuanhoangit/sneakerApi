using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Models.ProductEntities;

namespace SneakerAPI.AdminApi.Controllers.ProductControllers
{   
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductColorController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public ProductColorController(IUnitOfWork uow):base(uow)
        {
            _uow = uow;
        }
        [HttpGet("get/{id}")]
        public IActionResult GetProductColorById(int id){
            var productColor=_uow.ProductColor.Get(id);
            return productColor!=null?Ok(productColor):NotFound("notphao");
        }
        [HttpPost("create")]
        public IActionResult Create (ProductColor productColor){
            if(!ModelState.IsValid){
                return BadRequest(new {result=false,message="Add product color failed"});
            }
            var isSuccess = _uow.ProductColor.Add(productColor);
            return isSuccess? Ok(new {result=isSuccess,message="Added product color successfully"}):
            BadRequest(new {result=isSuccess,message="Add product color failed"});
        }
        [HttpPost("update")]
        public IActionResult Update (ProductColor productColor){
            if(!ModelState.IsValid){
                return BadRequest(new {result=false,message="Update product color failed"});
            }
            var isSuccess = _uow.ProductColor.Update(productColor);
            return isSuccess? Ok(new {result=isSuccess,message="Updated product color successfully"}):
            BadRequest(new {result=isSuccess,message="Update product color failed"});
        }
    }
}