//Tạo InterfaceRepo trước
using SneakerAPI.Core.Interfaces.OrderInterfaces;
using SneakerAPI.Core.Interfaces.ProductInterfaces;
using SneakerAPI.Core.Interfaces.UserInterfaces;

namespace SneakerAPI.Core.Interfaces;
public interface IUnitOfWork
{   
    //IOrder
    IOrderRepository Order { get; }
    IOrderDetailRepository OrderDetail { get; }
    //IProduct
    IProductRepository Product { get; }
    IProductTagRepository ProductTag { get; }
    ISizeRepository Size { get; }
    ICategoryRepository Category { get; }
    IBrandRepository Brand { get; }
    IColorRepository Color { get; }
    ITagRepository Tag { get; }
    IProductColorRepository ProductColor { get; }
    IProductColorSizeRepository ProductColorSize { get; }
    IProductCategoryRepository ProductCategory { get; }
    //IUser
    IAccountRepository Account { get; }
    IRoleRepository Role { get; }
    ICustomerInfoRepository CustomerInfo { get; }
    IStaffInfoRepository StaffInfo { get; }
    IAddressRepository Address { get; }
    void Save();
}