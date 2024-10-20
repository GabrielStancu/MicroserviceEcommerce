namespace Ordering.Domain.Models;

public class Product : Entity<Guid>
{
    public string Name => default!;
    public decimal Price => default!;
}
