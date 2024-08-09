namespace OrderAPI.Models {

    /// <summary>
    /// Represents an order in the system.
    /// </summary>
    public class Order {
        public int OrderId { get; set; }
        public required string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public required List<OrderItem> Items { get; set; }
    }
}