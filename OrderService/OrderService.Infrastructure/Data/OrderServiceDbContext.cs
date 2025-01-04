using Microsoft.EntityFrameworkCore;
using OrderService.Core;
using OrderService.Core.Models;
namespace OrderService.Infrastructure.Data
{
    public class OrderServiceDbContext : DbContext
    {
        public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options):base(options)
        {
            
        }
        public DbSet<AnEntity>? AnEntities {get;set;}
    }
}