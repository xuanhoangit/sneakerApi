using SneakerAPI.Core.Interfaces.OrderInterfaces;
using SneakerAPI.Core.Interfaces.ProductInterfaces;
using SneakerAPI.Core.Models.OrderEntities;
using SneakerAPI.Core.Models.ProductEntities;
using SneakerAPI.Infrastructure.Data;

namespace SneakerAPI.Infrastructure.Repositories.OrderRepositories;
public class OrderDetailRepository :Repository<OrderDetail> ,IOrderDetailRepository
{
    public OrderDetailRepository(SneakerAPIDbContext db):base(db)
    {
        
    }

}