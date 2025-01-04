using PaymentService.Core.Interfaces;
using PaymentService.Core.Models;
using PaymentService.Infrastructure.Data;

namespace PaymentService.Infrastructure.Repositories;
public class PaymentRepository :Repository<AnEntity> ,IPaymentRepository
{
    public PaymentRepository(PaymentServiceDbContext db):base(db)
    {
        
    }

}