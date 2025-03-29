using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Interfaces.OrderInterfaces;
using SneakerAPI.Core.Interfaces.ProductInterfaces;
using SneakerAPI.Core.Models.Filters;
using SneakerAPI.Core.Models.OrderEntities;
using SneakerAPI.Core.Models.ProductEntities;
using SneakerAPI.Infrastructure.Data;

namespace SneakerAPI.Infrastructure.Repositories.OrderRepositories;
public class OrderRepository :Repository<Order> ,IOrderRepository
{
    public OrderRepository(SneakerAPIDbContext db):base(db)
    {
        
    }

    public IQueryable<Order> GetOrderFiltered(OrderFilter filter)
    {   
        var query=_dbSet.AsQueryable();
        // if(filter.Order__Status)
        if(filter.RangePrice.MinPrice != null){
            query= query.Where(x=>x.Order__AmountDue>=filter.RangePrice.MinPrice);
        }
        if(filter.RangePrice.MaxPrice != null){
            query= query.Where(x=>x.Order__AmountDue<=filter.RangePrice.MaxPrice);
        }
        if(filter.RangeDateTime.From !=null){
            query =query.Where(x=>x.Order__CreatedDate>filter.RangeDateTime.From);
        }
        if(filter.RangeDateTime.From !=null){
            query =query.Where(x=>x.Order__CreatedDate>filter.RangeDateTime.To);
        }
        if(filter.Order__Status!=null){
            query= query.Where(x=>x.Order__Status==filter.Order__Status);
        }
        if(filter.Payment__Status!=null){
            query= query.Where(x=>x.Order__PaymentStatus==filter.Payment__Status);
        }
        return query;
    }
}