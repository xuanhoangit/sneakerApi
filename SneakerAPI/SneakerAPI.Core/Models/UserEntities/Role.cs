using System.ComponentModel.DataAnnotations;

namespace SneakerAPI.Core.Models.UserEntities
{
    public class Role
    {   
        [Key]
        public int Role__Id { get; set; }
        public string? Role__Name { get; set; }
    }
}