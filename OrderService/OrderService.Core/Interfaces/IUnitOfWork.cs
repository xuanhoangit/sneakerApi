//Tạo InterfaceRepo trước
namespace OrderService.Core.Interfaces;
public interface IUnitOfWork
{
    IOrderRepository Order { get; }

    void Save();
}