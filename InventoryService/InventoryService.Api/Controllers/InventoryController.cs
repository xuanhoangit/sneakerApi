using Microsoft.AspNetCore.Mvc;
using InventoryService.Core.Interfaces;


namespace InventoryService.Api.Controllers
{
    public class InventoryController : BaseController
    {
        private readonly IUnitOfWork uow;

        public InventoryController(IUnitOfWork uow) : base(uow)
        {
            this.uow = uow;
        }
        public IActionResult Get(){
            
            return Ok(uow.Inventory.GetAll());
        }
    }
}