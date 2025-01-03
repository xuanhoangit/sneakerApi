using System.ComponentModel.DataAnnotations;

namespace ProductService.Core.Models
{
    public class Product
    {   
        [Key]
        public int Product__Id { get; set; }
        [Required]
        public string? Product__Name { get; set; }
    }
}