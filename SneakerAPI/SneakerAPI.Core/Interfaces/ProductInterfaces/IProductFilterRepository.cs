using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Models;
using SneakerAPI.Core.Models.Filters;
using SneakerAPI.Core.Models.ProductEntities;

namespace SneakerAPI.Core.Interfaces.ProductInterfaces
{
    public interface IProductFilterRepository
    {
        Task<List<Product>> GetFilteredProductsAsync(ProductFilter filter);
    }
}