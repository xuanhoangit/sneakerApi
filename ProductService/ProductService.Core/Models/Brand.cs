using System.ComponentModel.DataAnnotations;

namespace ProductService.Core.Models
{
    public class Brand
    {   
        [Key]
        public int Brand__Id { get; set; }
        public string? Brand__Name { get; set; }
        public string? Brand__Description { get; set; }
        public string? Brand__Logo { get; set; }
        public string? Brand__Country { get; set; }
        public string? Brand__Website { get; set; }
        bool Brand__IsDeleted { get; set; }
    }
}