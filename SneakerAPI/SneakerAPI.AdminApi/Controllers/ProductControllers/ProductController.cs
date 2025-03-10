using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Models.Filters;
using SneakerAPI.Core.Models.ProductEntities;

namespace SneakerAPI.AdminApi.Controllers.ProductControllers
{   
    [ApiController]
    [Area("Admin")]
    [Route("api/[Controller]")]
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly int ProductEachPage=20;
        public ProductController(IUnitOfWork uow):base(uow)
        {
            _uow = uow;
        }
        [HttpGet("filter")]
        public IActionResult GetProductsByFilter([FromQuery] ProductFilter filter){
            return Ok(_uow.Product.GetFilteredProducts(filter).Skip((filter.Page-1)*ProductEachPage).Take(ProductEachPage).ToList());
        }
        [HttpGet("get-productsbycategory/{cateId}/{page}")]
        public IActionResult GetProductsByCategories(int cateId,int page=1){
            var category= _uow.Category.Get(cateId);
            if(category==null){
                return NotFound("Category is not exists");
            }
            var productIds= _uow.ProductCategory.GetAll(x=>x.ProductCategory__CategoryId==cateId).Select(x=>x.ProductCategory__ProductId).ToList();
            System.Console.WriteLine(productIds);
            if(productIds==null || productIds.Count()<=0){
                return NotFound("Have no product in this category");
            }
            var products=_uow.Product.GetAll(x=>productIds.Contains(x.Product__Id)).Skip((page-1)*ProductEachPage).Take(ProductEachPage);
            return Ok(products);
        }
        [HttpPut("realease-products")]
        public IActionResult ReleaseProducts(int[] productIds){
            var products= _uow.Product.GetAll(x=>
            productIds.Contains(x.Product__Id) && x.Product__Status==(int)Status.Unrelease);
            foreach (var p in products)
            {
                p.Product__Status=(int)Status.Active;
                var result=_uow.Product.Update(p);
            }
            return Ok();
        }
        [HttpGet("find-products/string={searchString},take={quantity}")]
        public IActionResult GetProductsByName(string searchString="",int quantity=0){
            var products=_uow.Product.GetAll(x=>x.Product__Name.Contains(searchString)).Take(quantity);
            return products.Count()>0
            ?Ok(products):NoContent();
        }
        [HttpGet("get-product/{id}")]
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
        [HttpPatch("delete-product/{id}")]
        public IActionResult Detele(int id){
            var product=_uow.Product.Get(id);
            //changeStatus
            product.Product__Status=(int)Status.Unactive;
            var isSuccess=_uow.Product.Update(product);
            return isSuccess?Ok(new {result=isSuccess,message="deleted product successfully"})
            :BadRequest(new {result=isSuccess,message="delete product failed"});
        }
    }
}