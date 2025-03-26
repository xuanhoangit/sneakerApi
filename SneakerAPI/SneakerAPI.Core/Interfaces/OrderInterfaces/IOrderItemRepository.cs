
using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Models.OrderEntities;

namespace SneakerAPI.Core.Interfaces.OrderInterfaces
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<List<OrderItemDTO>> GetOrderItem(int order_id);
    }
}