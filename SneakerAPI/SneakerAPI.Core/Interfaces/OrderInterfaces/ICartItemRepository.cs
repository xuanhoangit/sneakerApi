using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Models.OrderEntities;

namespace SneakerAPI.Core.Interfaces.OrderInterfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<List<CartItemDTO>> GetCartItem(int account_id, int[]? cartItem_Ids=null);
    }
}