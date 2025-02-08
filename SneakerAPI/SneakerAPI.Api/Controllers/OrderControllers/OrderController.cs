using SneakerAPI.Core.Interfaces;

namespace SneakerAPI.Api.Controllers.OrderControllers
{
    public class OrderController : BaseController
    {
        private readonly IUnitOfWork uow;

        public OrderController(IUnitOfWork uow):base(uow)
        {
            this.uow = uow;
        }
    }
}