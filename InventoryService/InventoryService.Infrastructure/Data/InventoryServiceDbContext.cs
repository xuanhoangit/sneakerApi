using Microsoft.EntityFrameworkCore;
using InventoryService.Core;
using InventoryService.Core.Models;
namespace InventoryService.Infrastructure.Data
{
    public class InventoryServiceDbContext : DbContext
    {
        public InventoryServiceDbContext(DbContextOptions<InventoryServiceDbContext> options):base(options)
        {
            
        }
        public DbSet<AnEntity>? AnEntities {get;set;}
    }
}