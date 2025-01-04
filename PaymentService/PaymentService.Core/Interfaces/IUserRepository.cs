using PaymentService.Core.Models;

namespace PaymentService.Core.Interfaces;
public interface IPaymentRepository : IRepository<AnEntity>
{
        // Task<bool> GetAll ();
}