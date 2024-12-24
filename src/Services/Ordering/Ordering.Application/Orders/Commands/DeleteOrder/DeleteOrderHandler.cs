﻿namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteOrderHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderId);
        var order = await _dbContext.Orders.FindAsync(new object[] { orderId },
            cancellationToken: cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }
}
