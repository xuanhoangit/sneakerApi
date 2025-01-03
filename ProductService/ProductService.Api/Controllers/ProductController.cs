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
        public IActionResult Get(){
            
            return Ok(uow.Product.GetAll());
        }
    }
}