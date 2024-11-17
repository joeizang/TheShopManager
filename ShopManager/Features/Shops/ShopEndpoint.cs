using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using NodaTime.Text;
using ShopManager.Data;
using ShopManager.Features.Shops.Filters;
using ShopManager.Features.Shops.Validations;

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
        
        shopGroup.MapGet("/all", async ([FromServices]ShopManagerBaseContext context) =>
        {
            var shops = new List<ShopDto>();

            await foreach (var shop in ShopQueryService.GetAllShops(context))
            {
                shops.Add(shop);
            }

            return Results.Ok(shops);
        });

        shopGroup.MapPost("", async ([FromServices]IShopCommandService command, [FromBody] CreateShopDto inputModel) =>
        {
            var result = await command.CreateShop(inputModel).ConfigureAwait(false);
            return TypedResults.Created();
        }).AddEndpointFilter<FilterCreateShop>()
        .Produces<ShopDto>(201)
        .Produces(400);

        shopGroupWithIds.MapPut("", async ([FromServices] IShopCommandService command,
            [FromBody] UpdateShopDto inputModel) =>
        {
            var result = await command.UpdateShop(inputModel).ConfigureAwait(false);
            return TypedResults.Ok();
        }).AddEndpointFilter<FilterUpdateShop>()
        .Produces<ShopDto>(200)
        .Produces(400);


        return shopGroup;
    }
}
