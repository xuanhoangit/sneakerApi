using System.ComponentModel.DataAnnotations;

namespace ProductService.Core.Models
{
    public class Color
    {   
        [Key]
        public int Color__Id { get; set; }
        public string? Color__Name { get; set; }
        public string? Color__Code { get; set; }
    }
}