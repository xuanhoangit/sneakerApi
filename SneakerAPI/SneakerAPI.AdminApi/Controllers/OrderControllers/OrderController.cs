using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakerAPI.Core.DTOs;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Core.Models.Filters;
using SneakerAPI.Core.Models.OrderEntities;

namespace SneakerAPI.AdminApi.Controllers.OrderControllers
{   
    [ApiController]
    [Route("api/orders")]
    [Authorize(Roles = RolesName.Customer)]
    public class OrderController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly int unitInAPage=20;

        public OrderController(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }
    
        [HttpGet("page/{page}")]
        public IActionResult GetOrders(int page=1)
        {
            var currentAccount = CurrentUser() as CurrentUser;
            if (currentAccount == null)
                return Unauthorized("User not authenticated.");

            var orders = _uow.Order.GetAll().Skip((page-1)*unitInAPage).Take(unitInAPage);

            if (!orders.Any())
                return NotFound("No orders found.");

            return Ok(orders);
        }
        [HttpGet("filter/page/{page}")]
        public IActionResult GetOrdersByFilter([FromBody] OrderFilter filter,int page=1)
        {
            var currentAccount = CurrentUser() as CurrentUser;
            if (currentAccount == null)
                return Unauthorized("User not authenticated.");

            var orders = _uow.Order.GetOrderFiltered(filter).Skip((page-1)*unitInAPage).Take(unitInAPage);

            if (!orders.Any())
                return NotFound("No orders found.");

            return Ok(orders);
        }

        [HttpGet("{orderId}/items")]
        public async Task<IActionResult> GetOrderItems(int orderId)
        {
            var currentAccount = CurrentUser() as CurrentUser;
            if (currentAccount == null)
                return Unauthorized("User not authenticated.");

            var order = _uow.Order.FirstOrDefault(x =>
                x.Order__CreatedByAccountId == currentAccount.AccountId && x.Order__Id == orderId);

            if (order == null)
                return NotFound("Order not found.");

            var orderItems = await _uow.OrderItem.GetOrderItem(orderId);
            return Ok(orderItems);
        }
        [HttpPatch("cancel-order/{orderId:int?}")]
        public async Task<IActionResult> CancelOrder(int orderId){
            try
            {
                var order=_uow.Order.Get(orderId);
                if(order==null){
                    return NotFound();
                }
                if(
                    order.Order__Status == (int)OrderStatus.Completed ||
                    order.Order__Status == (int)OrderStatus.Delivering ||
                    order.Order__Status == (int)OrderStatus.Delivered ){
                        //Không thể hủy
                        return Ok(new {message="Cannot cancel order. This order has been shipped"});
                }
                if(order.Order__PaymentStatus==(int)PaymentStatus.Unpaid ||
                    order.Order__Status==(int)OrderStatus.Pending ||
                    order.Order__Status==(int)OrderStatus.Processing){
                        // Hủy ngay
                        order.Order__Status=(int)OrderStatus.Cancelled;
                        return Ok(new {result=_uow.Order.Update(order),message="Order cancelled"});
                    }
                if(order.Order__PaymentStatus==(int)PaymentStatus.Paid && 
                    order.Order__Status != (int)OrderStatus.Completed &&
                    order.Order__Status != (int)OrderStatus.Delivering &&
                    order.Order__Status != (int)OrderStatus.Delivered){
                    // HỦy và hoàn tiền
                    order.Order__Status=(int)OrderStatus.Cancelled;
                    order.Order__PaymentStatus=(int)PaymentStatus.Refunding;
                    return Ok(new {
                        result=_uow.Order.Update(order),message="Order cancelled! Money will be refunded in a few days"
                    });
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutDTO checkoutDTO)
        {
            try
            {
                var currentAccount = CurrentUser() as CurrentUser;
                if (currentAccount == null)
                    return Unauthorized("User not authenticated.");

                if (checkoutDTO == null || checkoutDTO.CartItemIds == null || !checkoutDTO.CartItemIds.Any())
                    return BadRequest("Invalid cart items.");

                var cartItems = await _uow.CartItem.GetCartItem(currentAccount.AccountId, checkoutDTO.CartItemIds);

                if (!cartItems.Any())
                    return BadRequest("Cart is empty.");

                // Tạo đơn hàng
                var order = new Order
                {
                    Order__CreatedByAccountId = currentAccount.AccountId,
                    Order__CreatedDate=DateTime.Now,
                    Order__AmountDue = cartItems.Sum(c => c.ProductColor.ProductColor__Price * c.CartItem__Quantity),
                    OrderItems = cartItems.Select(c => new OrderItem
                    {
                        OrderItem__ProductColorSizeId = c.CartItem__ProductColorSizeId,
                        OrderItem__Quantity = c.CartItem__Quantity,
                    }).ToList(),
                    Order__PaymentCode = checkoutDTO.OrderPayment,
                    Order__Type = Form_of_purchase.Online,
                    Order__Status = (int)OrderStatus.Pending,
                    Order__PaymentStatus=(int)PaymentStatus.Unpaid
                };

                var result = _uow.Order.Add(order);
                if (result)
                {
                    _uow.CartItem.RemoveRange(_uow.CartItem.Find(x => checkoutDTO.CartItemIds.Contains(x.CartItem__Id)));
                    return Ok(new { Message = "Order placed successfully.", OrderId = order.Order__Id });
                }

                return BadRequest("Failed to place order.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
