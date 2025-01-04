using OrderService.Core.Models;

namespace OrderService.Core.Interfaces;
public interface IOrderRepository : IRepository<AnEntity>
{
        // Task<bool> GetAll ();
}