using SneakerAPI.Core.Interfaces;

namespace SneakerAPI.Api.Controllers.OrderControllers
{
    public class OrderDetailController : BaseController
    {
        private readonly IUnitOfWork uow;

        public OrderDetailController(IUnitOfWork uow):base(uow)
        {
            this.uow = uow;
        }
    }
}