using System;
using Microsoft.AspNetCore.Mvc;
using NodaTime.Text;
using ShopManager.Data;

namespace ShopManager.Features.Shops;

public static class EndpointHandler
{
    public static async Task<IResult> GetPaginatedShops([FromServices]ShopManagerBaseContext context,
        [FromQuery]string cursor)
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
    }
    
    public static async Task<IResult> GetAllShops([FromServices]ShopManagerBaseContext context)
    {
        var shops = new List<ShopDto>();
        await foreach (var shop in ShopQueryService.GetAllShops(context))
        {
            shops.Add(shop);
        }
        return Results.Ok(shops);
    }
    
    public static async Task<IResult> CreateShop([FromServices]IShopCommandService command, [FromBody] CreateShopDto inputModel)
    {
        var result = await command.CreateShop(inputModel).ConfigureAwait(false);
        return TypedResults.Created();
    }
    
    public static async Task<IResult> UpdateShop([FromServices] IShopCommandService command,
        [FromBody] UpdateShopDto inputModel)
    {
        var result = await command.UpdateShop(inputModel).ConfigureAwait(false);
        return TypedResults.Ok();
    }
    
    public static async Task<IResult> DeleteShop([FromServices] IShopCommandService command,
        [FromRoute] Guid shopId)
    {
        await command.DeleteShop(shopId).ConfigureAwait(false);
        return TypedResults.NoContent();
    }
}
