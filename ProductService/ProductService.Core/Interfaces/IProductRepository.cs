using ProductService.Core.Models;

namespace ProductService.Core.Interfaces;
public interface IProductRepository : IRepository<Product>
{
        // Task<bool> GetAll ();
}