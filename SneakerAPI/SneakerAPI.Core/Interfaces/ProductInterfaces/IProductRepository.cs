using SneakerAPI.Core.Models;
using SneakerAPI.Core.Models.ProductEntities;

namespace SneakerAPI.Core.Interfaces.ProductInterfaces;
public interface IProductRepository : IRepository<Product>
{
        // Task<bool> GetAll ();
}