using InventoryService.Core.Interfaces;
using InventoryService.Infrastructure.Data;

namespace InventoryService.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{   
    private readonly InventoryServiceDbContext _db;
    public UnitOfWork(InventoryServiceDbContext db)
    {
        _db=db;
        Inventory = new InventoryRepository(_db);
    }

    public IInventoryRepository Inventory {get;}


    public void Save()
    {
        _db.SaveChanges();
    }

}