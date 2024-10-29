namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name => default!;
    public string Email => default!;
}
