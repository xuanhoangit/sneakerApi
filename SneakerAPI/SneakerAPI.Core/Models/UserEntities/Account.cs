using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerAPI.Core.Models.UserEntities
{
    public class Account
    {   
        [Key]
        public int Account__Id { get; set; }
        public string? Account__Username { get; set; }
        public string? Account__PasswordHash { get; set; }
        public int? Account__RoleId { get; set; }
        [ForeignKey("Account__RoleId")]
        public Role? Role { get; set; }
        public bool? Account__IsActive { get; set; }
        public bool? Account__IsBlocked { get; set; }
    }
}