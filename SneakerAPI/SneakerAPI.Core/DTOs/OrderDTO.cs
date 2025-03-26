namespace SneakerAPI.Core.DTOs;
public class CheckoutDTO
{
    public int AccountId { get; set; }
    public long OrderPayment { get; set; }
    public int[] CartItemIds { get; set; }
}