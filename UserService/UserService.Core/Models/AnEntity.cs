using System.ComponentModel.DataAnnotations;

namespace UserService.Core.Models
{
    public class AnEntity
    {   
        [Key]
        public int AnEntity__Id { get; set; }
        [Required]
        public string? AnEntity__Name { get; set; }
    }
}