using ProductService.Core.Interfaces;
using ProductService.Core.Models;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories;
public class Product1Repository :Repository<AnEntity> ,IProduct1Repository
{
    public Product1Repository(ProductServiceDbContext db):base(db)
    {
        
    }

}