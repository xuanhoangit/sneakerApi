using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.Interfaces;

namespace SneakerAPI.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}