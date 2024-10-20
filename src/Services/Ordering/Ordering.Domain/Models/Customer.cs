namespace Ordering.Domain.Models;

public class Customer : Entity<Guid>
{
    public string Name => default!;
    public string Email => default!;
}
