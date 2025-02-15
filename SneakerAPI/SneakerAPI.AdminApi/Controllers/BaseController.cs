using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.Interfaces;

namespace SneakerAPI.AdminApi.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public BaseController(IUnitOfWork _uow)
        {
            uow = _uow;
        }
    }
}