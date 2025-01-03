using UserService.Core.Models;

namespace UserService.Core.Interfaces;
public interface IUserRepository : IRepository<AnEntity>
{
        // Task<bool> GetAll ();
}