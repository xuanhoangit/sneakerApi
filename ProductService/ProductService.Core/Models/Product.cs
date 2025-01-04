using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Core.Models
{
    public class Product
    {   
        [Key]
        public int Product__Id { get; set; }
        [Required]
        public string? Product__Name { get; set; }
        public int? Product__ImportPrice { get; set; }
        public int? Product__OriginPrice { get; set; }
        public byte? Product__DiscountPercent { get; set; }

        //Price = OriginPrice - (OriginPrice * DiscountPercent / 100)
        //Profit = Price - ImportPrice
        public int? Product__Price { get; set; }
        public int? Product__Profit { get; set; }
        public string? Product__Description { get; set; }
        public DateTime? Product__CreatedDate { get; set; }=DateTime.UtcNow;
        public DateTime? Product__UpdatedDate { get; set; }
        //
        public int Product__SubCategoryId { get; set; }
        [ForeignKey("Product__SubCategoryId")]
        public SubCategory? SubCategory { get; set; }
        public int Product__BrandId { get; set; }
        [ForeignKey("Product__BrandId")]
        public Brand? Brand { get; set; }
    }
}