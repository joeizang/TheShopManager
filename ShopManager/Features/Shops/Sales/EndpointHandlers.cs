using NodaTime;
using ShopManager.Data;

namespace ShopManager.Features.Shops.Sales;

public static class EndpointHandler
{
    public static IResult GetSales(ShopManagerBaseContext context, Guid shopId, Instant cursor, CancellationToken token)
    {
        var sales = SalesQueryService.GetSales(context, shopId, cursor, token);
        return Results.Ok(sales);
    }

    public static IResult GetSaleById(ShopManagerBaseContext context, Guid saleId, Guid shopId, CancellationToken token)
    {
        var sale = SalesQueryService.GetSaleById(context, saleId, shopId, token);
        return Results.Ok(sale);
    }
}
