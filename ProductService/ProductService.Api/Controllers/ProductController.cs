using Microsoft.AspNetCore.Mvc;
using ProductService.Core.Interfaces;


namespace ProductService.Api.Controllers
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
        [HttpGet]
        public IActionResult GetById(int Id){
            return Ok(uow.Product.Get(Id));
        }
        [HttpGet("getmessage")]
        public IActionResult GetMessage(){
            return Ok("PRODUCT À NHON");
        }
        [HttpGet("searchproduct/{Name}")] 
        public async Task<IActionResult> SearchProductByName(string Name){
            return Ok(await uow.Product.GetAllAsync(p=>p.Product__Name.Contains(Name)));
        }
    }
}