using Microsoft.AspNetCore.Mvc;
using UserService.Core.Interfaces;


namespace UserService.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUnitOfWork uow;

        public UserController(IUnitOfWork uow) : base(uow)
        {
            this.uow = uow;
        }
        public IActionResult Get(){
            
            return Ok(uow.User.GetAll());
        }
    }
}