using Microsoft.AspNetCore.Mvc;
using OrderAPI.Models;
using OrderAPI.Services;
using System.Threading.Channels;

namespace OrderAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase {

        private readonly IOrderService _orderService;
        private readonly Channel<PaymentInfo> _paymentChannel;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, Channel<PaymentInfo> paymentChannel, ILogger<OrdersController> logger) {
            _orderService = orderService;
            _paymentChannel = paymentChannel;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="orderDto">Order data transfer object</param>
        /// <returns>Created order</returns>
        [HttpPost("Create a new order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDto) {
            var createdOrder = await _orderService.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        /// <returns>List of orders</returns>
        [HttpGet("Get all orders")]
        public async Task<IActionResult> GetOrders() {
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Processes payment for an order.
        /// </summary>
        /// <param name="paymentInfo">Payment information</param>
        /// <returns>No content</returns>
        [HttpPost("Payment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentInfo paymentInfo) {
            await _paymentChannel.Writer.WriteAsync(paymentInfo);
            _logger.LogInformation($"Payment info for order {paymentInfo.OrderId} received.");
            return NoContent();
        }

        /// <summary>
        /// Retrieves a specific order by ID.
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>Order details</returns>
        [HttpGet("Get order by ID")]
        public async Task<IActionResult> GetOrderById(int id) {
            var order = await _orderService.GetOrderByIdAsync(id);
            if(order == null) {
                _logger.LogWarning($"Order {id} not found.");
                return NotFound();
            }
            return Ok(order);
        }
    }
}
