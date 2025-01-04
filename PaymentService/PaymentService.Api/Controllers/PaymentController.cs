using Microsoft.AspNetCore.Mvc;
using PaymentService.Core.Interfaces;


namespace PaymentService.Api.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IUnitOfWork uow;

        public PaymentController(IUnitOfWork uow) : base(uow)
        {
            this.uow = uow;
        }
        public IActionResult Get(){
            
            return Ok(uow.Payment.GetAll());
        }
    }
}