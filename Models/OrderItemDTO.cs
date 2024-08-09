namespace OrderAPI.Models {

    /// <summary>
    /// Data Transfer Object for OrderItem.
    /// </summary>
    public class OrderItemDTO {
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}