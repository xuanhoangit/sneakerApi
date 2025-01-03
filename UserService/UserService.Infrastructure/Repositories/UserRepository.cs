using UserService.Core.Interfaces;
using UserService.Core.Models;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;
public class UserRepository :Repository<AnEntity> ,IUserRepository
{
    public UserRepository(UserServiceDbContext db):base(db)
    {
        
    }

}