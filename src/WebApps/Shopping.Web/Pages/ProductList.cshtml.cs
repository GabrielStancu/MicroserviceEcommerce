namespace Shopping.Web.Pages;

public class ProductListModel : PageModel
{
    private readonly IBasketService _basketService;
    private readonly ICatalogService _catalogService;
    private readonly ILogger<ProductListModel> _logger;
    public IEnumerable<string> CategoryList { get; set; } = new List<string>();
    public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();

    [BindProperty(SupportsGet = true)]
    public string SelectedCategory { get; set; } = default!;

    public ProductListModel(IBasketService basketService,
        ICatalogService catalogService, 
        ILogger<ProductListModel> logger)
    {
        _basketService = basketService;
        _catalogService = catalogService;
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync(string categoryName)
    {
        var response = await _catalogService.GetProducts();

        CategoryList = response.Products.SelectMany(p => p.Category).Distinct();

        if (!string.IsNullOrWhiteSpace(categoryName))
        {
            ProductList = response.Products.Where(p => p.Category.Contains(categoryName));
            SelectedCategory = categoryName;
        }
        else
        {
            ProductList = response.Products;
        }

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
            Quantity = 1,
            Color = "Black"
        });

        await _basketService.StoreBasket(new StoreBasketRequest(basket));

        return RedirectToPage("Cart");
    }
}