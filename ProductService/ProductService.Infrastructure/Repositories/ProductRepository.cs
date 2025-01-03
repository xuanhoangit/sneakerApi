using ProductService.Core.Interfaces;
using ProductService.Core.Models;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repositories;
public class ProductRepository :Repository<Product> ,IProductRepository
{
    public ProductRepository(ProductServiceDbContext db):base(db)
    {
        
    }

}