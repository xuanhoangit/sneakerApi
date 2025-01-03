//Tạo InterfaceRepo trước
namespace UserService.Core.Interfaces;
public interface IUnitOfWork
{
    IUserRepository User { get; }

    void Save();
}