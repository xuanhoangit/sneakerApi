using OrderService.Core.Interfaces;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{   
    private readonly OrderServiceDbContext _db;
    public UnitOfWork(OrderServiceDbContext db)
    {
        _db=db;
        Order = new OrderRepository(_db);
    }

    public IOrderRepository Order {get;}


    public void Save()
    {
        _db.SaveChanges();
    }

}