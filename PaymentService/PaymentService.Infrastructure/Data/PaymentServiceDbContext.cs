using Microsoft.EntityFrameworkCore;
using PaymentService.Core;
using PaymentService.Core.Models;
namespace PaymentService.Infrastructure.Data
{
    public class PaymentServiceDbContext : DbContext
    {
        public PaymentServiceDbContext(DbContextOptions<PaymentServiceDbContext> options):base(options)
        {
            
        }
        public DbSet<AnEntity>? AnEntities {get;set;}
    }
}