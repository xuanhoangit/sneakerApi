using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public IActionResult GetBrandById(int id){
            var brand=_uow.Brand.Get(id);
            return brand!=null?
            Ok(brand):NotFound("NotFound");
        }
        [HttpGet("get-brands/search={searchString},take={quantity}")]
        public IActionResult GetBrands(string searchString="",int quantity=0){
            var brands=_uow.Brand.GetAll(x=> x.Brand__Name.Contains(searchString)).Take(quantity);
            return brands.Count()>0?
            Ok(brands):NotFound("NotFound");
        }
    }
}