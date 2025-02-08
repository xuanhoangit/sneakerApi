using Microsoft.AspNetCore.Mvc;
// using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Interfaces;

using SneakerAPI.Core.Models.ProductEntities;


namespace SneakerAPI.Api.Controllers
{   
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork uow;
        public ProductController(IUnitOfWork uow) : base(uow)
        {
            this.uow = uow;
        }
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id){
            return Ok(uow.Product.Get(id));
        }

        [HttpGet("searchproduct/{name}")] 
        public async Task<IActionResult> SearchProductByName(string name){
            //validate
            
            //
            return Ok(await uow.Product.GetAllAsync(p=>p.Product__Name.Contains(name)));
        }

        [HttpPost("create")]
        public IActionResult Create(Product product){
            //validate

            //
            var result=uow.Product.Add(product);
            return Ok(new {isSuccess=result,message="Thêm sản phẩm thành công"});
        }
        [HttpPut("update")]
        public IActionResult Update(Product product){
            //validate
           
            //
            var result=uow.Product.Update(product);
            return Ok(new {isSuccess=result,message="Cập nhật sản phẩm thành công"});
        }
        [HttpPatch("delete/{id}")]
        public IActionResult Delete(int id){
            //validate
           
            //
            var product=uow.Product.Get(id);
            var result=uow.Product.Update(product);
            return Ok(new {isSuccess=result,message="Xóa sản phẩm thành công"});
        }
    }
}