using OrderService.Core.Interfaces;
using OrderService.Core.Models;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repositories;
public class OrderRepository :Repository<AnEntity> ,IOrderRepository
{
    public OrderRepository(OrderServiceDbContext db):base(db)
    {
        
    }

}