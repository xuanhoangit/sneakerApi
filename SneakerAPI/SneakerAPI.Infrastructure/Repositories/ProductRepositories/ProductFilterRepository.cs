using Microsoft.EntityFrameworkCore;
using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Interfaces.ProductInterfaces;
using SneakerAPI.Core.Models.Filters;
using SneakerAPI.Core.Models.ProductEntities;
using SneakerAPI.Infrastructure.Data;


namespace ProductService.Infrastructure.Repositories
{
    public class ProductFilterRepository : IProductFilterRepository
    {
        private static SneakerAPIDbContext _db;
        public ProductFilterRepository(SneakerAPIDbContext db)
        {
            _db = db;
        }

public async Task<List<Product>> GetFilteredProductsAsync(ProductFilter filter)
{
    using (var context = _db) // Thay bằng DbContext của bạn
    {
        var query = context.Products.AsQueryable();

        // 1. Lọc theo SearchString
        if (!string.IsNullOrEmpty(filter.SearchString))
        {
            query = query.Where(p => p.Product__Name.Contains(filter.SearchString) ||
                                     p.Product__Description.Contains(filter.SearchString));
        }

        // 2. Lọc theo ColorIds (Dùng JOIN thay vì ProductColors trực tiếp)
        if (filter.ColorIds != null && filter.ColorIds.Length > 0)
        {
            var colorIdList = filter.ColorIds.ToList(); // Chuyển về List<int>

            query = query.Where(p => context.ProductColors
                            .Any(pc => colorIdList.Contains(pc.ProductColor__ColorId) &&
                                       pc.ProductColor__ProductId == p.Product__Id));
        }

        return await query.ToListAsync();
    }
}

    }

}