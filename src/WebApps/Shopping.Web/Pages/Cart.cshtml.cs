using Microsoft.Extensions.Logging;
using Shopping.Web.Services;

namespace Shopping.Web.Pages;

public class CartModel : PageModel
{
    private readonly IBasketService _basketService;
    private readonly ILogger<CartModel> _logger;

    public CartModel(IBasketService basketService, ILogger<CartModel> logger)
    {
        _basketService = basketService;
        _logger = logger;
    }

    public ShoppingCartModel Cart { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await _basketService.LoadUserBasket();

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
    {
        _logger.LogInformation("Remove to cart button clicked");
        Cart = await _basketService.LoadUserBasket();
        Cart.Items.RemoveAll(x => x.ProductId == productId);

        await _basketService.StoreBasket(new StoreBasketRequest(Cart));

        return RedirectToPage();
    }
}