using UserService.Core.Interfaces;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{   
    private readonly UserServiceDbContext _db;
    public UnitOfWork(UserServiceDbContext db)
    {
        _db=db;
        User = new UserRepository(_db);
    }

    public IUserRepository User {get;}


    public void Save()
    {
        _db.SaveChanges();
    }

}