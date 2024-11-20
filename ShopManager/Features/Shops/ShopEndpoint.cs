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

        shopGroup.MapGet("", EndpointHandler.GetPaginatedShops);
        
        shopGroup.MapGet("/all", EndpointHandler.GetAllShops);

        shopGroup.MapPost("", EndpointHandler.CreateShop)
            .AddEndpointFilter<FilterCreateShop>().Produces<ShopDto>(201).Produces(400);

        shopGroupWithIds.MapPut("", EndpointHandler.UpdateShop)
            .AddEndpointFilter<FilterUpdateShop>()
            .Produces<ShopDto>(200).ProducesValidationProblem(400);

        shopGroupWithIds.MapDelete("", EndpointHandler.DeleteShop)
            .AddEndpointFilter<FilterDeleteShop>()
            .Produces(204).ProducesValidationProblem(400);
        
        return shopGroup;
    }
}
