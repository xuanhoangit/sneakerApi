using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;


namespace OrderService.Api.Controllers
{   
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : BaseController
    {
        private readonly IUnitOfWork uow;

        public OrderController(IUnitOfWork uow) : base(uow)
        {
            this.uow = uow;
        }
        [HttpGet("getall")]
        public IActionResult GetAll(){
            
            return Ok("HHAHHAHAH");
        }
    }
}