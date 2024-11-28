using FluentValidation;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public record GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;

public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);

public class GetOrdersByIdQueryValidator : AbstractValidator<GetOrdersByNameQuery>
{
    public GetOrdersByIdQueryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name must not be empty");
    }
}