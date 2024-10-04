using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly DiscountContext _dbContext;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
            return new CouponModel { Amount = 0, ProductName = "No Discount", Description = "No Discount description" };

        _logger.LogInformation("Discount is retrieved for Product {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        _dbContext.Coupons.Add(coupon);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Discount is successfully created. Product: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

        _dbContext.Coupons.Update(coupon);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Discount is successfully updated. Product: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount for Product {request.ProductName} could not be found."));

        _dbContext.Coupons.Remove(coupon);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("Discount  for Product {ProductName} was deleted", coupon.ProductName);

        return new DeleteDiscountResponse { Success = true };
    }
}
