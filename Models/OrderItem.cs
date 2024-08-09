namespace OrderAPI.Models {

    /// <summary>
    /// Represents an item within an order.
    /// </summary>
    public class OrderItem {
        public int OrderItemId { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}