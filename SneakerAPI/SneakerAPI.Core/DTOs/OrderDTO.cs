namespace SneakerAPI.Core.DTOs;
public class CheckoutDTO
{
    public int AccountId { get; set; }
    public long OrderPayment { get; set; }
    public int[] CartItemIds { get; set; }
}
public class OrderDTO
{
    //  [Key]
        public int Order__Id { get; set; }
        public int Order__CreatedByAccountId { get; set; }//FK
        // [ForeignKey("Order__CreatedByAccountId")]
        // public IdentityAccount? Account { get; set; }
        public int Order__Status { get; set; }
        public int Order__PaymentStatus { get; set; }
        public decimal Order__AmountDue { get; set; }//Số tiền phải trả
        public long Order__PaymentCode { get; set; }
        public DateTime Order__CreatedDate { get; set; }
        public int Order__PaymentMethod {get;set;}= (int)PaymentMethod.Cash_on_Delivery;
        public string? Order__Type {get;set;}//Mua on hay mua off
        public virtual List<OrderItemDTO>? OrderItems {get;set;}
}