using Microsoft.EntityFrameworkCore;
using SneakerAPI.Core;
using SneakerAPI.Core.Models.OrderEntities;
using SneakerAPI.Core.Models.ProductEntities;
using SneakerAPI.Core.Models.UserEntities;
namespace SneakerAPI.Infrastructure.Data
{
    public class SneakerAPIDbContext : DbContext
    {
        public SneakerAPIDbContext(DbContextOptions<SneakerAPIDbContext> options):base(options)
        {
            
        }
        //ORDERENTITIES
        public DbSet<Order>? Orders {get;set;}
        public DbSet<OrderDetail>? OrderDetails {get;set;}
        //PRODUCTENTITIES
        public DbSet<Product>? Products {get;set;}
        public DbSet<ProductColor>? ProductColors {get;set;}
        public DbSet<ProductColorSize>? ProductColorSizes {get;set;}
        public DbSet<Tag>? Tags {get;set;}
        public DbSet<ProductTag>? ProductTags {get;set;}
        public DbSet<Color>? Colors {get;set;}
        public DbSet<Size>? Sizes {get;set;}
        public DbSet<Category>? Categories {get;set;}
        public DbSet<ProductCategory>? ProductCategories {get;set;}
        public DbSet<Brand>? Brands {get;set;}
        //USERENTITIES
        public DbSet<Account>? Accounts {get;set;}
        public DbSet<Address>? Addresses {get;set;}
        public DbSet<CustomerInfo>? CustomerInfos {get;set;}
        public DbSet<StaffInfo>? StaffInfos {get;set;}
        public DbSet<Role>? Roles {get;set;}
    }
}