using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Core.Models
{
    public class File
    {   
        [Key]
        public int File__Id { get; set; }
        public string? File__Name { get; set; }
        public string? File__Path { get; set; }
        public string? File__Extension { get; set; }
        public string? File__Size { get; set; }
        public string? File__Type { get; set; }
        public int File__ProductColorId { get; set; }
        [ForeignKey("File__ProductColorId")]
        public ProductColor? ProductColor { get; set; }
    }
}