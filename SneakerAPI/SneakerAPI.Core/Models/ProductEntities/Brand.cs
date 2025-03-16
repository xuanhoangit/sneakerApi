using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerAPI.Core.Models.ProductEntities
{
    public class Brand
    {   
        [Key]
        public int Brand__Id { get; set; }
        [Required]
        public string? Brand__Name { get; set; }
        [Required]
        public string? Brand__Description { get; set; }
        [Required]
        public string? Brand__Logo { get; set; }
        // [Required]
        // public int Brand__CreatedByAccountId  { get; set; }
        // [ForeignKey("Brand__CreatedByAccountId")]
        // public IdentityAccount? IdentityAccount {get;set;}
        public bool Brand__Status { get; set; }
    }
}