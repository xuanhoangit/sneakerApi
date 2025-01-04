using System.ComponentModel.DataAnnotations;

namespace ProductService.Core.Models
{
    public class Category
    {   
        [Key]
        public int Category__Id { get; set; }
        public string? Category__Name { get; set; }
        public string? Category__Description { get; set; }
    }
}