using SneakerAPI.Core.Interfaces.OrderInterfaces;
using SneakerAPI.Core.Interfaces.ProductInterfaces;
using SneakerAPI.Core.Models.OrderEntities;
using SneakerAPI.Core.Models.ProductEntities;
using SneakerAPI.Infrastructure.Data;

namespace SneakerAPI.Infrastructure.Repositories.OrderRepositories;
public class OrderRepository :Repository<Order> ,IOrderRepository
{
    public OrderRepository(SneakerAPIDbContext db):base(db)
    {
        
    }

}