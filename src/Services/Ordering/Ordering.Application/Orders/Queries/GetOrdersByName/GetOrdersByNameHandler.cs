namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    private readonly IApplicationDbContext _dbContext;

    public GetOrdersByNameHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);

        var orderDtos = ProjectToOrderDtos(orders);

        return new GetOrdersByNameResult(orderDtos);
    }

    private IEnumerable<OrderDto> ProjectToOrderDtos(List<Order> orders)
    {
        var result = new List<OrderDto>();

        foreach (var order in orders)
        {
            var orderDto = new OrderDto(order.Id.Value,
                order.CustomerId.Value,
                order.OrderName.Value,
                new AddressDto(
                    order.ShippingAddress.FirstName,
                    order.ShippingAddress.LastName,
                    order.ShippingAddress.EmailAddress,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.Country,
                    order.ShippingAddress.State,
                    order.ShippingAddress.ZipCode),
                new AddressDto(
                    order.BillingAddress.FirstName,
                    order.BillingAddress.LastName,
                    order.BillingAddress.EmailAddress,
                    order.BillingAddress.AddressLine,
                    order.BillingAddress.Country,
                    order.BillingAddress.State,
                    order.BillingAddress.ZipCode),
                new PaymentDto(
                    order.Payment.CardName,
                    order.Payment.CardNumber,
                    order.Payment.Expiration,
                    order.Payment.Cvv,
                    order.Payment.PaymentMethod),
                order.Status,
                order.OrderItems.Select(oi => new OrderItemDto(
                    oi.OrderId.Value,
                    oi.ProductId.Value,
                    oi.Quantity,
                    oi.Price)).ToList());

            result.Add(orderDto);
        }

        return result;
    }
}
