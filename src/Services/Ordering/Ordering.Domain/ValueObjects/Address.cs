namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName => default!;
    public string LastName => default!;
    public string EmailAddress => default!;
    public string AddressLine => default!;
    public string Country => default!;
    public string State => default!;
    public string ZipCode => default!;
}
