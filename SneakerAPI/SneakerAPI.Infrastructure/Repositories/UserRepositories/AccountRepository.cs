using SneakerAPI.Infrastructure.Repositories;
using SneakerAPI.Core.Interfaces.UserInterfaces;
using SneakerAPI.Core.Models.UserEntities;
using SneakerAPI.Infrastructure.Data;

namespace SneakerAPI.Infrastructure.Repositories.UserRepositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(SneakerAPIDbContext context) : base(context)
        {
        }
    }
}