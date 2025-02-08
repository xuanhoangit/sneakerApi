using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Interfaces.OrderInterfaces;
using SneakerAPI.Core.Interfaces.ProductInterfaces;
using SneakerAPI.Core.Interfaces.UserInterfaces;
using SneakerAPI.Infrastructure.Data;
using SneakerAPI.Infrastructure.Repositories.OrderRepositories;
using SneakerAPI.Infrastructure.Repositories.ProductRepositories;
using SneakerAPI.Infrastructure.Repositories.UserRepositories;

namespace SneakerAPI.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{   
    private readonly SneakerAPIDbContext _db;
    public UnitOfWork(SneakerAPIDbContext db)
    {
        _db=db;
        //Order
        Order = new OrderRepository(_db);
        OrderDetail = new OrderDetailRepository(_db);
        //Product
        Product = new ProductRepository(_db);
        ProductTag = new ProductTagRepository(_db);
        Size = new SizeRepository(_db);
        Category = new CategoryRepository(_db);
        Brand = new BrandRepository(_db);
        Color = new ColorRepository(_db);
        Tag = new TagRepository(_db);
        ProductColor = new ProductColorRepository(_db);
        ProductColorSize = new ProductColorSizeRepository(_db);
        ProductCategory = new ProductCategoryRepository(_db);
        //User
        Account = new AccountRepository(_db);
        Role = new RoleRepository(_db);
        CustomerInfo = new CustomerInfoRepository(_db);
        StaffInfo = new StaffInfoRepository(_db);
        Address = new AddressRepository(_db);
    }

    public IOrderRepository Order {get;}

    public IOrderDetailRepository OrderDetail {get;}

    public IProductRepository Product {get;}

    public IProductTagRepository ProductTag {get;}

    public ISizeRepository Size {get;}

    public ICategoryRepository Category {get;}

    public IBrandRepository Brand {get;}

    public IColorRepository Color {get;}

    public ITagRepository Tag {get;}

    public IProductColorRepository ProductColor {get;}

    public IProductColorSizeRepository ProductColorSize {get;}

    public IProductCategoryRepository ProductCategory {get;}

    public IAccountRepository Account {get;}

    public IRoleRepository Role {get;}

    public ICustomerInfoRepository CustomerInfo {get;}

    public IStaffInfoRepository StaffInfo {get;}

    public IAddressRepository Address {get;}

    // public IOrderRepository Order {get;}


    public void Save()
    {
        _db.SaveChanges();
    }

}