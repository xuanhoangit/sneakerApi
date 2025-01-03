using Microsoft.EntityFrameworkCore;
using ProductService.Core;
using ProductService.Core.Models;
namespace ProductService.Infrastructure.Data
{
    public class ProductServiceDbContext : DbContext
    {
        public ProductServiceDbContext(DbContextOptions<ProductServiceDbContext> options):base(options)
        {
            
        }
        public DbSet<AnEntity>? AnEntities {get;set;}
    }
}