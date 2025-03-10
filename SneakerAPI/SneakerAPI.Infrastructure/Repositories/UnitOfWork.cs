using Microsoft.Extensions.Configuration;
using ProductService.Infrastructure.Repositories;
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
    // private readonly IConfiguration _config;
    public UnitOfWork(SneakerAPIDbContext db)
    {
        _db=db;
        //Order
        Order = new OrderRepository(_db);
        OrderDetail = new OrderDetailRepository(_db);
        //Product
        ProductColorFile = new ProductColorFileRepository(_db);
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

        CustomerInfo = new CustomerInfoRepository(_db);
        StaffInfo = new StaffInfoRepository(_db);
        Address = new AddressRepository(_db);
        ProductFilter = new ProductFilterRepository(_db);
    }
    public IProductFilterRepository ProductFilter { get; }
    public IOrderRepository Order {get;}

    public IOrderDetailRepository OrderDetail {get;}

    public IProductColorFileRepository ProductColorFile {get;}
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




    public ICustomerInfoRepository CustomerInfo {get;}

    public IStaffInfoRepository StaffInfo {get;}

    public IAddressRepository Address {get;}



    // public IOrderRepository Order {get;}


    public void Save()
    {
        _db.SaveChanges();
    }

}