//Tạo InterfaceRepo trước
namespace ProductService.Core.Interfaces;
public interface IUnitOfWork
{
    IProduct1Repository Product1 { get; }

    void Save();
}