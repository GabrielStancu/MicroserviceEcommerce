namespace Shopping.Web.Pages;

public class OrderListModel : PageModel
{
    private readonly IOrderingService _orderingService;
    public IEnumerable<OrderModel> Orders { get; set; } = default!;

    public OrderListModel(IOrderingService orderingService)
    {
        _orderingService = orderingService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        // assumption customerId is passed in from the UI authenticated user swn
        var customerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");

        var response = await _orderingService.GetOrdersByCustomer(customerId);
        Orders = response.Orders;

        return Page();
    }
}