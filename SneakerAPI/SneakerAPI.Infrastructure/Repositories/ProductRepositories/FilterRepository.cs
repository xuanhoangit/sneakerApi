// using Microsoft.EntityFrameworkCore;
// using ProductService.Core.DTOs;
// using ProductService.Core.Interfaces;
// using ProductService.Core.Models;
// using ProductService.Infrastructure.Data;

// namespace ProductService.Infrastructure.Repositories
// {
//     public class FilterRepository : IFilterRepository
//     {
//         private ProductServiceDbContext _db;

//         public FilterRepository(ProductServiceDbContext db)
//         {
//             _db = db;
//         }
//         public async Task<List<Product>> GetProductsAsync(ProductFilter filter)
//         {
//             var query = new List<ProductColor>().AsQueryable();

//             // Lọc theo ColorIds

//             var productQuery= from product in _db.Products join 
//             productColor in _db.ProductColors on product.Product__Id equals productColor.ProductColor__ProductId
//             join productColorSize in _db.ProductColorSizes on productColor.ProductColor__Id equals productColorSize.ProductColorSize__ProductColorId
//             join size in _db.Sizes on productColorSize.ProductColorSize__SizeId equals size.Size__Id
//             join color in _db.Colors on productColor.ProductColor__ColorId equals color.Color__Id
//             where filter.ProductColorIds.Contains(color.Color__Id) && filter.ProductColorSizeIds.Contains(size.Size__Id)
//             && productColor.ProductColor__Price >= filter.RangePrice.MinPrice && productColor.ProductColor__Price <= filter.RangePrice.MaxPrice
//             select new Product
//             {
//                 Product__Id = product.Product__Id,
//                 Product__Name = product.Product__Name,
//                 ProductColors = product.ProductColors.Select(p => new ProductColor
//                 {
//                     ProductColor__Id = p.ProductColor__Id,
//                     ProductColor__Price = p.ProductColor__Price,
//                     ProductColorSizes = p.ProductColorSizes.Select(p => new ProductColorSize
//                     {
//                         ProductColorSize__Id = p.ProductColorSize__Id,
//                         ProductColorSize__SizeId = p.ProductColorSize__SizeId,
//                         ProductColorSize__Quantity = p.ProductColorSize__Quantity
//                     }).ToList()
//                 }).ToList()
//             };
//             // if (filter.ColorIds != null && filter.ColorIds.Count()>0)
//             // {
//             //     query = query.Where(p => filter.ColorIds.Contains(p.ProductColor__Id));
//             // }
//             // if (filter.SizeIds != null && filter.SizeIds.Count()>0)
//             // {
//             //     query = query.Where(p => filter.ColorIds.Contains(p.ProductColor__Id));
//             // }
//             return await productQuery.ToListAsync();
//         }
//     }
// }