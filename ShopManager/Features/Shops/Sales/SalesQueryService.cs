using Microsoft.EntityFrameworkCore;
using NodaTime;
using ShopManager.Data;

namespace ShopManager.Features.Shops.Sales;

public static class SalesQueryService
{
    public static readonly Func<ShopManagerBaseContext, Guid, Instant, CancellationToken, IEnumerable<SaleSummaryDto>> GetSales = 
        EF.CompileQuery(
            (ShopManagerBaseContext context, Guid shopId, Instant cursor, CancellationToken token) =>
            context
                .Sales
                .AsNoTracking()
                .Where(s => s.ShopId == shopId && s.CreatedAt > cursor)
                .OrderBy(s => s.CreatedAt)
                .Select(s => new SaleSummaryDto(
                    s.Id, s.CreatedAt.ToDateTimeUtc().ToString(), s.TotalAmount.Amount))
                .Take(10)
        );

    public static readonly Func<ShopManagerBaseContext, Guid, Guid, CancellationToken, Task<SaleDetailDto?>> GetSaleById = 
        EF.CompileQuery(
            (ShopManagerBaseContext context, Guid saleId, Guid shopId, CancellationToken token) =>
            context
                .Sales
                .AsNoTracking()
                .Where(s => s.Id == saleId && s.ShopId == shopId)
                .OrderByDescending(s => s.CreatedAt)
                .Select(s => 
                new SaleDetailDto(s.Id, s.SalesPerson.Email ?? "", s.CreatedAt.ToDateTimeUtc().ToString(), 
                s.TotalAmount.Amount, s.SaleItems.Select(si => new SaleItemDto(si.ProductId, si.Product.ProductName, si.QuantitySold, si.UnitPrice.Amount)).ToArray(), 0, 0, $"{s.Customer.FirstName} {s.Customer.LastName}"))
                .FirstOrDefaultAsync(token)
        );
}