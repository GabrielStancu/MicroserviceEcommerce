using Core.CQRS;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        // create order entity from the command object
        // save the database
        // return the result
        throw new NotImplementedException();
    }
}
