//Tạo InterfaceRepo trước
namespace ProductService.Core.Interfaces;
public interface IUnitOfWork
{
    IProductRepository Product { get; }

    void Save();
}