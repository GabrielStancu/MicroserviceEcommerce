namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardName => default!;
    public string CardNumber => default!;
    public string Expiration => default!;
    public string Cvv => default!;
    public int PaymentMethod => default!;
}
