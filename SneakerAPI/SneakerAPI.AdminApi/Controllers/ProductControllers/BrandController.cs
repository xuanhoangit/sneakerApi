using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.Interfaces;

namespace SneakerAPI.AdminApi.Controllers.ProductControllers
{
    public class BrandController : BaseController
    {
        private readonly IUnitOfWork _uow;

        public BrandController(IUnitOfWork uow):base(uow)
        {
            _uow = uow;
        }
        [HttpGet("get-brand/{id}")]
        public IActionResult GetBrandByIb(int id){
            var brand=_uow.Brand.Get(id);
            return brand!=null?
            Ok(brand):NotFound("NotFound");
        }
    }
}