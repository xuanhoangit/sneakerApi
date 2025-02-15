using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.Interfaces;

namespace SneakerAPI.Api.Controllers.ProductControllers
{   
    [Authorize]
    [Area("customer")]
    public class ProductColorController : BaseController
    {
        private readonly IUnitOfWork uow;

        public ProductColorController(IUnitOfWork uow) : base(uow)
        {
            this.uow = uow;
        }
    }
}