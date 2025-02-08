using System.ComponentModel.DataAnnotations;

namespace SneakerAPI.Core.Models.OrderEntities
{
    public class OrderDetail
    {   
        [Key]
        public int OrderDetail__Id { get; set; }
        public int OrderDetail__OrderId { get; set; }//FK
        public Order? Order { get; set; }
        public int OrderDetail__ProductColorSizeId { get; set; }//FK
        public int OrderDetail__Quantity { get; set; }
        public decimal OrderDetail__UnitPrice { get; set; }
        public decimal OrderDetail__TotalAmount { get; set; }
    }
}
