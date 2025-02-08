using System.ComponentModel.DataAnnotations;

namespace SneakerAPI.Core.Models.ProductEntities
{
    public class ProductCategory
    {   
        [Key]
        public int ProductCategory__Id { get; set; }
        public int? ProductCategory__ProductId { get; set; }
        public Product? Product { get; set; }
        public int? ProductCategory__CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}