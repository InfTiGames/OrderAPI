namespace OrderAPI.Models {

    /// <summary>
    /// Data Transfer Object for Order.
    /// </summary>
    public class OrderDTO {
        public int OrderId { get; set; }
        public required string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public required List<OrderItemDTO> Items { get; set; }
    }
}