namespace Basket.API.Data;

public class CachedBasketRepository : IBasketRepository
{
    private readonly IBasketRepository _repository;

    public CachedBasketRepository(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        return await _repository.GetBasket(userName, cancellationToken);
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        return await _repository.StoreBasket(basket, cancellationToken);
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteBasket(userName, cancellationToken);
    }
}
