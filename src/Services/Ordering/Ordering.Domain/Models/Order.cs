namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public CustomerId CustomerId { get; set; } = default!;
    public OrderName OrderName => default!;
    public Address ShippingAddress => default!;
    public Address BillingAddress => default!;
    public Payment Payment => default!;
    public OrderStatus Status => OrderStatus.Pending;

    public decimal TotalPrice => OrderItems.Sum(x => x.Price * x.Quantity);
}
