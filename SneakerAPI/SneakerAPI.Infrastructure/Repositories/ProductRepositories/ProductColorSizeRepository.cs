using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Interfaces.ProductInterfaces;
using SneakerAPI.Core.Models;
using SneakerAPI.Core.Models.ProductEntities;
using SneakerAPI.Infrastructure.Data;

namespace SneakerAPI.Infrastructure.Repositories.ProductRepositories;
public class ProductColorSizeRepository :Repository<ProductColorSize> ,IProductColorSizeRepository
{
    public ProductColorSizeRepository(SneakerAPIDbContext db):base(db)
    {
        
    }
    // public 
}