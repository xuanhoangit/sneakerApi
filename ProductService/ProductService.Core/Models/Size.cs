using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Core.Models
{
    public class Size
    {   
        [Key]
        public int Size__Id { get; set; }
        public string? Size__Name { get; set; }
        public string? Size__Quantity { get; set; }
        public int Size__ProductColorId { get; set; }
        [ForeignKey("Size__ProductColorId")]
        public ProductColor? ProductColor { get; set; }
    }
}