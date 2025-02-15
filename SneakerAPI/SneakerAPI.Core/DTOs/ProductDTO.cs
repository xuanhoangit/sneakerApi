
using SneakerAPI.Core.Models.ProductEntities;
using SneakerAPI.Core.Models.UserEntities;

namespace SneakerAPI.Core.DTOs
{
    public class ProductDTO
    {
        // [Key]
        public int Product__Id { get; set; }
        public string? Product__Name { get; set; }
        public string? Product__Description { get; set; }
        public DateTime? Product__CreatedDate { get; set; }
        public DateTime? Product__UpdatedDate { get; set; }
        public Account? Account {get;set;}
        public List<ProductColor>? ProductColors { get; set; }
        public Brand? Brand {get;set;}
        public Category? Category {get;set;}
        public Color? Color {get;set;}

    }
}