namespace SneakerAPI.Core.DTOs;
public class CartDTO
{
        public int CartItem__Id { get; set; }
        public int CartItem__Quantity {get;set;}
        public int CartItem__ProductColorSizeId { get; set; }//FK

}