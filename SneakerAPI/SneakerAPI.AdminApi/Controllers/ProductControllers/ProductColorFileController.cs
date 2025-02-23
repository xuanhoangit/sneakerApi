using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Models.ProductEntities;

namespace SneakerAPI.AdminApi.Controllers.ProductControllers
{       
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductColorFileController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly string uploadFilePath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/uploads/product-imgs");

        public ProductColorFileController(IUnitOfWork uow):base(uow)
        {
            _uow = uow;
        }
        [HttpPost("uploadFile")]
        public async Task<IActionResult> Upload([FromForm] List<ProductColorFile> productFiles)
        {   System.Console.WriteLine(productFiles[0].ProductColorFile__Name);
            System.Console.WriteLine(productFiles[0].ProductColorFile__Id);
            if (productFiles[0].ProductColorFile__File == null || productFiles.Count() == 0)
                return BadRequest("No files were uploaded.");

            var result=_uow.ProductColorFile.AddRange(productFiles);
            if(!result){
                return Ok(new {isSuccess=result,message="Thất bại"});
            }

            foreach (var file in productFiles)
            {
                var filePath = Path.Combine(uploadFilePath, file.ProductColorFile__Name!);
                System.Console.WriteLine(productFiles[0].ProductColorFile__Name);
                System.Console.WriteLine(filePath);
                // Lưu file vào thư mục server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.ProductColorFile__File.CopyToAsync(stream);
                }
                
            }
            var fileUrls=await _uow.ProductColorFile.GetAllAsync(x=>x.ProductColorFile__ProductColorId==productFiles[0].ProductColorFile__ProductColorId);
                        foreach (var file in fileUrls)
                    {
                        file.Url= $"{Request.Scheme}://{Request.Host}/uploads/product-imgs/{file.ProductColorFile__Name}";
                    }
            return Ok(new { isSuccess = true, fileUrls });
        }

    }
}