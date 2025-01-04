using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Core.Models
{
    public class ProductColor
    {   
        [Key]
        public int ProductColor__Id { get; set; }
        public int? ProductColor__ColorId { get; set; }
        [ForeignKey("ProductColor__ColorId")]
        public ProductColor? Color { get; set; }
        public int ProductColor__ProductId { get; set; }
        [ForeignKey("ProductColor__ProductId")]
        public Product? Product { get; set; }
    }
}