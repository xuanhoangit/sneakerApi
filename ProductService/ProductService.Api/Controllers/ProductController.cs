using Microsoft.AspNetCore.Mvc;
using ProductService.Core.Interfaces;


namespace ProductService.Api.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork uow;
        public ProductController(IUnitOfWork uow) : base(uow)
        {
            this.uow = uow;
        }
        public IActionResult GetById(int Id){
            return Ok(uow.Product.Get(Id));
        }
        public async Task<IActionResult> SearchProductByName(string Name){
            return Ok(await uow.Product.GetAllAsync(p=>p.Product__Name.Contains(Name)));
        }
    }
}