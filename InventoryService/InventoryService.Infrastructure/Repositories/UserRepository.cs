using InventoryService.Core.Interfaces;
using InventoryService.Core.Models;
using InventoryService.Infrastructure.Data;

namespace InventoryService.Infrastructure.Repositories;
public class InventoryRepository :Repository<AnEntity> ,IInventoryRepository
{
    public InventoryRepository(InventoryServiceDbContext db):base(db)
    {
        
    }

}