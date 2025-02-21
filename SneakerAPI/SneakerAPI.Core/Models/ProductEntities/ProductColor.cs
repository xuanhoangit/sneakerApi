using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerAPI.Core.Models.ProductEntities
{
    public class ProductColor
    {   
        [Key]
        public int ProductColor__Id { get; set; }
        public decimal ProductColor__Price { get; set; }
        public int ProductColor__ColorId { get; set; }
        [ForeignKey("ProductColor__ColorId")]
        public  Color? Color { get; set; }
        public int ProductColor__ProductId { get; set; }
        [ForeignKey("ProductColor__ProductId")]
        public  Product? Product { get; set; }
        // [NotMapped]
        public virtual ICollection<ProductColorSize>? ProductColorSizes { get; set; }
    }
}