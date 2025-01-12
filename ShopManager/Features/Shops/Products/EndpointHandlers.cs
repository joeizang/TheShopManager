using Microsoft.AspNetCore.Mvc;
using ShopManager.Core;
using ShopManager.Data;

namespace ShopManager.Features.Shops.Products;

public static class EndpointHandler
{
    public static async Task<IResult> GetProducts(Guid shopId,
        string cursor, [FromServices] ShopManagerBaseContext context, CancellationToken token)
    {
        var result = await ProductsQueryService
            .GetProducts(context, shopId, cursor.ToInstantDate(), token)
            .ConfigureAwait(false);
        return TypedResults.Ok(result);
    }
}
