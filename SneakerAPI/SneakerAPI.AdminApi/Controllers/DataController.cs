using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Models;
using SneakerAPI.Core.Models.ProductEntities;

namespace SneakerAPI.AdminApi.Controllers.ProductControllers
{   
public static class SeedRoleAdmin
{
public static async Task CreateRoles(IServiceProvider serviceProvider)
{
    // Lấy RoleManager và UserManager từ DI container
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityAccount>>();
    var uow = serviceProvider.GetRequiredService<IUnitOfWork>();
    // Định nghĩa các role cần seed
    string[] roleNames = { "Admin", "Customer", "Manager","Staff" };

    foreach (var roleName in roleNames)
    {
        // Kiểm tra xem role đã tồn tại hay chưa
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole<int>(roleName));
        }
    }

    // Thông tin tài khoản admin
    var adminEmail = "admin@gmail.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        // Tạo tài khoản admin nếu chưa tồn tại
        var newAdminUser = new IdentityAccount
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };
        string adminPassword = "Admin@123"; // Lưu ý: sử dụng mật khẩu mạnh và phù hợp
        var createAdmin = await userManager.CreateAsync(newAdminUser, adminPassword);
        if (createAdmin.Succeeded)
        {
            // Gán role Admin cho tài khoản vừa tạo
            await userManager.AddToRoleAsync(newAdminUser, "Admin");
            

            var admin = await userManager.FindByEmailAsync(adminEmail);
             var seedbrand=uow.Brand.AddRange(DataGenerator.GetBrands(admin.Id));
            var seedcategory=uow.Category.AddRange(DataGenerator.GetCategories());
            var seedcolor=uow.Color.AddRange(DataGenerator.GetColors());
            var seedsize=uow.Size.AddRange(DataGenerator.GetSizes());

            var brands=uow.Brand.GetAll().ToList();
            var categories=uow.Category.GetAll().ToList();
            var colors=uow.Color.GetAll().ToList();
            var sizes=uow.Size.GetAll().ToList();

            var seedproduct=uow.Product.AddRange(DataGenerator.GetProducts(brands));
            var products=uow.Product.GetAll().ToList();

            var seedproductcategory=uow.ProductCategory.AddRange(DataGenerator.GetProductCategories(products, categories));
            
            var seedproductcolor=uow.ProductColor.AddRange(DataGenerator.GetProductColors(products, colors));
            var productColors=uow.ProductColor.GetAll().ToList();

            var seedproductcolorsize=uow.ProductColorSize.AddRange(DataGenerator.GetProductColorSizes(productColors, sizes));
 
        }
    }
}

}
public class DataGenerator
{
    public static List<Brand> GetBrands(int accountId)
    {
        return new List<Brand>
        {
            new Brand { Brand__Name = "Brand A", Brand__Description = "High quality brand A", Brand__Logo = "logoA.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = true },
            new Brand { Brand__Name = "Brand B", Brand__Description = "Premium brand B", Brand__Logo = "logoB.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = false },
            new Brand { Brand__Name = "Brand C", Brand__Description = "Luxury brand C", Brand__Logo = "logoC.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = true },
            new Brand { Brand__Name = "Brand D", Brand__Description = "Affordable brand D", Brand__Logo = "logoD.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = false },
            new Brand { Brand__Name = "Brand E", Brand__Description = "Reliable brand E", Brand__Logo = "logoE.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = true },
            new Brand { Brand__Name = "Brand F", Brand__Description = "Trusted brand F", Brand__Logo = "logoF.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = false },
            new Brand { Brand__Name = "Brand G", Brand__Description = "Quality brand G", Brand__Logo = "logoG.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = true },
            new Brand { Brand__Name = "Brand H", Brand__Description = "Popular brand H", Brand__Logo = "logoH.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = false },
            new Brand { Brand__Name = "Brand I", Brand__Description = "Innovative brand I", Brand__Logo = "logoI.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = true },
            new Brand { Brand__Name = "Brand J", Brand__Description = "Exclusive brand J", Brand__Logo = "logoJ.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = false },
            new Brand { Brand__Name = "Brand K", Brand__Description = "Top-rated brand K", Brand__Logo = "logoK.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = true },
            new Brand { Brand__Name = "Brand L", Brand__Description = "Leading brand L", Brand__Logo = "logoL.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = false },
            new Brand { Brand__Name = "Brand M", Brand__Description = "Popular brand M", Brand__Logo = "logoM.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = true },
            new Brand { Brand__Name = "Brand N", Brand__Description = "Affordable brand N", Brand__Logo = "logoN.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = false },
            new Brand { Brand__Name = "Brand O", Brand__Description = "Trusted brand O", Brand__Logo = "logoO.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = true },
            new Brand { Brand__Name = "Brand P", Brand__Description = "Premium brand P", Brand__Logo = "logoP.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = false },
            new Brand { Brand__Name = "Brand Q", Brand__Description = "Exclusive brand Q", Brand__Logo = "logoQ.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = true },
            new Brand { Brand__Name = "Brand R", Brand__Description = "Reliable brand R", Brand__Logo = "logoR.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = false },
            new Brand { Brand__Name = "Brand S", Brand__Description = "Top-rated brand S", Brand__Logo = "logoS.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = true },
            new Brand { Brand__Name = "Brand T", Brand__Description = "Innovative brand T", Brand__Logo = "logoT.png"/*, Brand__CreatedByAccountId = accountId*/, Brand__Status = false }
        };
    }

    public static List<Category> GetCategories()
    {
        var categories = new List<Category>();
        for (int i = 1; i <= 20; i++)
        {
            categories.Add(new Category { Category__Name = $"Category {i}" });
        }
        return categories;
    }

    public static List<Color> GetColors()
    {
        var colors = new List<Color>();
        string[] colorNames = { "Red", "Blue", "Green", "Yellow", "Purple", "Orange", "Pink", "Brown", "Black", "White", "Gray", "Cyan", "Magenta", "Lime", "Teal", "Indigo", "Violet", "Gold", "Silver", "Beige" };
        for (int i = 1; i <= 20; i++)
        {
            colors.Add(new Color { Color__Name = colorNames[i - 1] });
        }
        return colors;
    }
     public static List<Product> GetProducts(List<Brand> brands)
    {
        var products = new List<Product>();
        for (int i = 1; i <= 20; i++)
        {
            products.Add(new Product
            {
                Product__Name = $"Product {i}",
                Product__Description = $"Description for product {i}",
                Product__CreatedByAccountId = 1,
                Product__BrandId = brands[i - 1].Brand__Id,
                Product__CreatedDate = DateTime.Now.AddDays(-i),
                Product__UpdatedDate = DateTime.Now.AddDays(-i / 2)
            });
        }
        return products;
    }
    public static List<ProductColor> GetProductColors(List<Product> products, List<Color> colors)
    {
        var productColors = new List<ProductColor>();
        for (int i = 1; i <= 20; i++)
        {
            productColors.Add(new ProductColor
            {
                ProductColor__Price = 100 + i,
                ProductColor__ColorId = colors[1].Color__Id,
                ProductColor__ProductId = products[i - 1].Product__Id
            });
        }
        return productColors;
    }
    public static List<ProductColorSize> GetProductColorSizes(List<ProductColor> productColors,List<Size> sizes)
    {
        var productColorSizes = new List<ProductColorSize>();
        for (int i = 1; i <= 20; i++)
        {
            productColorSizes.Add(new ProductColorSize
            {
                ProductColorSize__SizeId = sizes[1].Size__Id,
                ProductColorSize__Quantity = 100 + i,
                ProductColorSize__ProductColorId = productColors[i - 1].ProductColor__Id
            });
        }
        return productColorSizes;
    }
    public static List<Size> GetSizes()
    {
        var sizes = new List<Size>();
        for (int i = 1; i <= 10; i++)
        {
            sizes.Add(new Size { 
                Size__Value = (35 + i).ToString()
             });
        }
        return sizes;
    }
    public static List<ProductCategory> GetProductCategories(List<Product> products, List<Category> categories)
    {
        var productCategories = new List<ProductCategory>();
        for (int i = 1; i <= 20; i++)
        {
            productCategories.Add(new ProductCategory
            {
                ProductCategory__ProductId = products[i - 1].Product__Id,
                ProductCategory__CategoryId = categories[i - 1].Category__Id
            });
        }
        return productCategories;
    }
// }   
    }

}