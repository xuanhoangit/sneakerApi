using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Models.ProductEntities;

namespace SneakerAPI.AdminApi.Controllers.ProductControllers
{
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public ProductController(IUnitOfWork uow):base(uow)
        {
            _uow = uow;
        }
        [HttpGet("get-product")]
        public IActionResult GetProductById(int id){
            var product = _uow.Product.Get(id);
            return product!=null?
            Ok(product):NotFound("NotFound");
        }
        [HttpPost("create-product")]
        public IActionResult Create([FromBody]Product product){
            if(!ModelState.IsValid){
                return BadRequest(new {result=false,message="add product failed"});
            }
            var isSuccess=_uow.Product.Add(product);
            return isSuccess?Ok(new {result=isSuccess,message="added product successfully"})
            :BadRequest(new {result=isSuccess,message="add product failed"});
        }
        [HttpPut("update-product")]
        public IActionResult Update([FromBody]Product product){
            if(!ModelState.IsValid){
                return BadRequest(new {result=false,message="update product failed"});
            }
            var isSuccess=_uow.Product.Update(product);
            return isSuccess?Ok(new {result=isSuccess,message="updated product successfully"})
            :BadRequest(new {result=isSuccess,message="update product failed"});
        }
        [HttpPatch("delete-product")]
        public IActionResult Detele(Product product){
            // product.Status=1;
            var isSuccess=_uow.Product.Update(product);
            return isSuccess?Ok(new {result=isSuccess,message="deleted product successfully"})
            :BadRequest(new {result=isSuccess,message="delete product failed"});
        }
    }
}