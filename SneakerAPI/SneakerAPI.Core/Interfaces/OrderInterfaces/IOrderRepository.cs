
using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Models.Filters;
using SneakerAPI.Core.Models.OrderEntities;

namespace SneakerAPI.Core.Interfaces.OrderInterfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetOrderFiltered(OrderFilter filter);
    }
}