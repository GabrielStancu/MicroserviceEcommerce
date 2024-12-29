namespace Shopping.Web.Pages;

public class ProductDetailModel : PageModel
{
    private readonly IBasketService _basketService;
    private readonly ICatalogService _catalogService;
    private readonly ILogger<ProductDetailModel> _logger;

    public ProductDetailModel(IBasketService basketService,
        ICatalogService catalogService,
        ILogger<ProductDetailModel> logger)
    {
        _basketService = basketService;
        _catalogService = catalogService;
        _logger = logger;
    }
    public ProductModel Product { get; set; } = default!;

    [BindProperty]
    public string Color { get; set; } = default!;

    [BindProperty]
    public int Quantity { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid productId)
    {
        var response = await _catalogService.GetProduct(productId);
        Product = response.Product;

        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        _logger.LogInformation("Add to cart button clicked");
        var productResponse = await _catalogService.GetProduct(productId);

        var basket = await _basketService.LoadUserBasket();

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Price = productResponse.Product.Price,
            Quantity = Quantity,
            Color = Color
        });

        await _basketService.StoreBasket(new StoreBasketRequest(basket));

        return RedirectToPage("Cart");
    }
}