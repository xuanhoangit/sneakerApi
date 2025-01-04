
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Core.Models
{
    public class SubCategory
    {   
        [Key]
        public int SubCategory__Id { get; set; }
        public string? SubCategory__Name { get; set; }
        public string? SubCategory__Description { get; set; }
        public int? SubCategory__CategoryId { get; set; }
        [ForeignKey("SubCategory__CategoryId")]
        public Category? Category { get; set; }
    }
}