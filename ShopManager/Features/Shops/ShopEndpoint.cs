using ShopManager.Features.Shops.Categories;
using ShopManager.Features.Shops.Categories.Filters;
using ShopManager.Features.Shops.Filters;
using ShopManager.Features.Shops.Products;
using ShopManager.Features.Shops.Products.Filters;
using Shops = ShopManager.Features.Shops;

namespace ShopManager.Features.Shops;

public static class ShopEndpoints
{
    public static RouteGroupBuilder MapShopEndpoints(this IEndpointRouteBuilder routes)
    {
        var shopGroup = routes.MapGroup("/api/v1/shops");
        var shopGroupWithIds = shopGroup.MapGroup("/{shopId:guid}");

        shopGroup.MapGet("", EndpointHandler.GetPaginatedShops);
        
        shopGroup.MapGet("/all", EndpointHandler.GetAllShops);

        shopGroup.MapPost("", EndpointHandler.CreateShop)
            .AddEndpointFilter<FilterCreateShop>()
            .Produces<ShopDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        shopGroupWithIds.MapPut("", EndpointHandler.UpdateShop)
            .AddEndpointFilter<FilterUpdateShop>()
            .Produces<ShopDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        shopGroupWithIds.MapDelete("", EndpointHandler.DeleteShop)
            .AddEndpointFilter<FilterDeleteShop>()
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);
        
        // Categories
        CategoryEndpoints.AddCategoryEndpoints(shopGroupWithIds);
        
        // Products
        ProductEndpoints.AddProductEndpoints(shopGroupWithIds);
        
        return shopGroup;
    }
}
