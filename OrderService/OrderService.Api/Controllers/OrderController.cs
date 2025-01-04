using Microsoft.AspNetCore.Mvc;
using OrderService.Core.Interfaces;


namespace OrderService.Api.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IUnitOfWork uow;

        public OrderController(IUnitOfWork uow) : base(uow)
        {
            this.uow = uow;
        }
        public IActionResult Get(){
            
            return Ok(uow.Order.GetAll());
        }
    }
}