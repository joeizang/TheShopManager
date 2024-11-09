using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTime.Text;
using ShopManager.Data;

namespace ShopManager.Features.Shops;

public static class ShopEndpoints
{
    public static RouteGroupBuilder MapShopEndpoints(this IEndpointRouteBuilder routes)
    {
        var shopGroup = routes.MapGroup("/api/v1/shops");
        var shopGroupWithIds = shopGroup.MapGroup("/{shopId:Guid}");

        shopGroup.MapGet("", async ([FromServices]ShopManagerBaseContext context,
            [FromQuery]string cursor) =>
        {
            if (InstantPattern.ExtendedIso.Parse(cursor.ToString()) is not { Success: true, Value: var parsedCursor })
            {
                return Results.BadRequest("Invalid cursor");
            }
            var shops = new List<ShopDto>();

            await foreach (var shop in ShopQueryService.GetShops(context, parsedCursor))
            {
                shops.Add(shop);
            }

            return Results.Ok(shops);
        });


        return shopGroup;
    }
}
