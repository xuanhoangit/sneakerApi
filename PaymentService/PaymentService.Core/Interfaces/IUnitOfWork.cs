//Tạo InterfaceRepo trước
namespace PaymentService.Core.Interfaces;
public interface IUnitOfWork
{
    IPaymentRepository Payment { get; }

    void Save();
}