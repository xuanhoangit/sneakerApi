using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SneakerAPI.Core.Models.UserEntities;

namespace SneakerAPI.Core.Models.OrderEntities
{
    public class Order
    {   
        [Key]
        public int Order__Id { get; set; }
        public int Order__CreatedByAccountId { get; set; }//FK
        [ForeignKey("Order__CreatedByAccountId")]
        public IdentityAccount? Account { get; set; }
        public int Order__Status { get; set; }
        public decimal Order__Amount { get; set; }
        public decimal Order__Total { get; set; }
        public DateTime Order__CreatedDate { get; set; }
    }
}
