using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Interfaces.ProductInterfaces;
using SneakerAPI.Core.Models;
using SneakerAPI.Core.Models.Filters;
using SneakerAPI.Core.Models.ProductEntities;
using SneakerAPI.Infrastructure.Data;

namespace SneakerAPI.Infrastructure.Repositories.ProductRepositories;
public class ProductRepository :Repository<Product> ,IProductRepository
{
    private readonly SneakerAPIDbContext _db;

    // protected DbSet<Product> _productSet;

    public ProductRepository(SneakerAPIDbContext db):base(db)
    {
        _db = db;
        // _productSet=this.db.Set<Product>();
    }
    // public IEnumerable<ProductDTO> GetProductsByCategoryId(int cate)
    //     {
    //         var query=from product in _db.Products
    //         join productCategory in _db.ProductCategories on product.Product__Id equals productCategory.ProductCategory__ProductId
    //         join category in _db.Categories on productCategory.ProductCategory__Id equals category.Category__Id
    //         join productColor in _db.ProductColors on product.Product__Id equals productColor.ProductColor__ProductId
    //         join
    //         return query.ToList();
    //     }
    public IQueryable<Product> GetFilteredProducts(ProductFilter filter)
    {
        var query = _context.Set<Product>()
            .Where(p => p.Product__Status == (int)Status.Unrelease || p.Product__Status==(int)Status.Active) // Chỉ lấy sản phẩm đã phát hành
            .AsQueryable();
         // Lọc theo Brand (thương hiệu)
        if (filter.BrandIds != null && filter.BrandIds.Any())
        {
            query = query.Where(p => filter.BrandIds.Contains(p.Product__BrandId));
        }

        // Lọc theo Color (màu sắc)
        if (filter.ColorIds != null && filter.ColorIds.Any())
        {
            query = query.Where(p => p.ProductColors.Any(pc => filter.ColorIds.Contains(pc.ProductColor__ColorId)));
        }

        // // Lọc theo Size (kích thước)
        // if (filter.SizeNames != null && filter.SizeNames.Any())
        // {
        //     query = query.Where(p => p.ProductColors
        //         .Any(pc => pc.ProductColorSizes
        //         .Any(pcs => filter.SizeNames.Contains(pcs.ProductColorSize__SizeName))));
        // }

        // Lọc theo khoảng giá
        if (filter.RangePrice != null)
        {
            if (filter.RangePrice.MinPrice.HasValue)
            {
                query = query.Where(p => p.ProductColors.Any(pc => pc.ProductColor__Price >= filter.RangePrice.MinPrice.Value));
            }

            if (filter.RangePrice.MaxPrice.HasValue)
            {
                query = query.Where(p => p.ProductColors.Any(pc => pc.ProductColor__Price <= filter.RangePrice.MaxPrice.Value));
            }
        }

        return query;
    }

}