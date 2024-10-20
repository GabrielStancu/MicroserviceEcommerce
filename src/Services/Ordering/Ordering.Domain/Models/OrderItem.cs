namespace Ordering.Domain.Models;

public class OrderItem : Entity<Guid>
{
    internal OrderItem(Guid orderId, Guid productId, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public Guid OrderId { get; }
    public Guid ProductId { get; }
    public int Quantity { get; }
    public decimal Price { get; }
}
