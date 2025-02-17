using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Interfaces.ProductInterfaces;
using SneakerAPI.Core.Models;
using SneakerAPI.Core.Models.ProductEntities;
using SneakerAPI.Infrastructure.Data;

namespace SneakerAPI.Infrastructure.Repositories.ProductRepositories;
public class ProductRepository :Repository<Product> ,IProductRepository
{
    private readonly SneakerAPIDbContext db;

    public ProductRepository(SneakerAPIDbContext db):base(db)
    {
        this.db = db;
    }
    public List<Product> GetProductsByCategories(int cateId){
        // var products=from product in db.Products
        // join productCategory in db.ProductCategories on product.Product__Id equals productCategory.ProductCategory__ProductId
        // join category in db.Categories on product
        return null;
    }
}