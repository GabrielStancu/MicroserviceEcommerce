namespace Ordering.Domain.Models;

public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public Guid CustomerId { get; set; } = default!;
    public string OrderName => default!;
    public Address ShippingAddress => default!;
    public Address BillingAddress => default!;
    public Payment Payment => default!;
    public OrderStatus Status => OrderStatus.Pending;

    public decimal TotalPrice => OrderItems.Sum(x => x.Price * x.Quantity);
}
