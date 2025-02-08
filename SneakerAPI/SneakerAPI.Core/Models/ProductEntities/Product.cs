using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SneakerAPI.Core.Models.UserEntities;

namespace SneakerAPI.Core.Models.ProductEntities
{
    public class Product
    {   
        [Key]
        public int Product__Id { get; set; }
        [Required]
        public string? Product__Name { get; set; }
        public string? Product__Description { get; set; }
        public DateTime? Product__CreatedDate { get; set; }=DateTime.Now;
        public DateTime? Product__UpdatedDate { get; set; }
        //
        public int Product__CreatedByAccountId { get; set; } // FK tham chiếu đến User trong microservice User
        [ForeignKey("Product__CreatedByAccountId")]
        public Account?  Account { get; set; }
        public int Product__BrandId { get; set; }
        [ForeignKey("Product__BrandId")]
        public Brand? Brand { get; set; }
        [NotMapped]
        public List<ProductColor>? ProductColors { get; set; }
    }
}