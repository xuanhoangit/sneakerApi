using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.Interfaces;

namespace SneakerAPI.AdminApi.Controllers.ProductControllers
{
    public class CategoryController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public CategoryController(IUnitOfWork uow):base(uow)
        {
            _uow = uow;
        }
        [HttpGet("get-categories/{quantity}")]
        public IActionResult GetCategories(int quantity=0){
            var categories=_uow.Category.GetAll().Take(quantity);
            return Ok(categories);
        }
    }
}