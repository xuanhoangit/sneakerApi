//Tạo InterfaceRepo trước
namespace InventoryService.Core.Interfaces;
public interface IUnitOfWork
{
    IInventoryRepository Inventory { get; }

    void Save();
}