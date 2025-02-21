using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SneakerAPI.Core.Models.ProductEntities
{
    public class ProductColorFile
    {   
        [Key]
        public int ProductColorFile__Id { get; set; }
        public string? ProductColorFile__Name { get; set; }=Guid.NewGuid().ToString()+".jpg";
        // public string? File__Path { get; set; }
        // public string? File__Extension { get; set; }
        // public string? File__Size { get; set; }
        // public string? File__Type { get; set; }
        [NotMapped]
        public IFormFile? ProductColorFile__File {get;set;}
        [NotMapped]
        public string? Url { get; set; }
        public int ProductColorFile__ProductColorId { get; set; }
        [ForeignKey("ProductColorFile__ProductColorId")]
        public ProductColor? ProductColor { get; set; }
    }
}