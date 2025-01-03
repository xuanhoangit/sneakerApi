using Microsoft.EntityFrameworkCore;
using UserService.Core;
using UserService.Core.Models;
namespace UserService.Infrastructure.Data
{
    public class UserServiceDbContext : DbContext
    {
        public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options):base(options)
        {
            
        }
        public DbSet<AnEntity>? AnEntities {get;set;}
    }
}