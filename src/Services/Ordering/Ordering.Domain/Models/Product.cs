namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name => default!;
    public decimal Price => default!;
}
