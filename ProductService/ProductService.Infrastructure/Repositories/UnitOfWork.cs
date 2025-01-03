using ProductService.Core.Interfaces;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{   
    private readonly ProductServiceDbContext _db;
    public UnitOfWork(ProductServiceDbContext db)
    {
        _db=db;
        Product = new ProductRepository(_db);
    }

    public IProductRepository Product {get;}


    public void Save()
    {
        _db.SaveChanges();
    }

}