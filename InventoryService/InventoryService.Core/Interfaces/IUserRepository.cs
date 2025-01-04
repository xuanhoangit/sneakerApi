using InventoryService.Core.Models;

namespace InventoryService.Core.Interfaces;
public interface IInventoryRepository : IRepository<AnEntity>
{
        // Task<bool> GetAll ();
}