using PaymentService.Core.Interfaces;
using PaymentService.Infrastructure.Data;

namespace PaymentService.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{   
    private readonly PaymentServiceDbContext _db;
    public UnitOfWork(PaymentServiceDbContext db)
    {
        _db=db;
        Payment = new PaymentRepository(_db);
    }

    public IPaymentRepository Payment {get;}


    public void Save()
    {
        _db.SaveChanges();
    }

}