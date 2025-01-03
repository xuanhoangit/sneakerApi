using ProductService.Core.Interfaces;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{   
    private readonly ProductServiceDbContext _db;
    public UnitOfWork(ProductServiceDbContext db)
    {
        _db=db;
        Product1 = new Product1Repository(_db);
    }

    public IProduct1Repository Product1 {get;}


    public void Save()
    {
        _db.SaveChanges();
    }

}