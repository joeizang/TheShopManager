using ShopManager.Features.Shops.Categories;
using ShopManager.Features.Shops.Categories.Filters;
using ShopManager.Features.Shops.Filters;
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
            .AddEndpointFilter<FilterCreateShop>().Produces<ShopDto>(201).Produces(400);

        shopGroupWithIds.MapPut("", EndpointHandler.UpdateShop)
            .AddEndpointFilter<FilterUpdateShop>()
            .Produces<ShopDto>(200).ProducesValidationProblem(400);

        shopGroupWithIds.MapDelete("", EndpointHandler.DeleteShop)
            .AddEndpointFilter<FilterDeleteShop>()
            .Produces(204).ProducesValidationProblem(400);
        
        // Categories
        shopGroupWithIds.MapPost("/categories", EndpointHandlers.CreateCategory)
            .AddEndpointFilter<FilterCreateCategory>()
            .Produces<CategoryDto>(201)
            .Produces(400)
            .Produces(500);
        
        shopGroupWithIds.MapPut("/categories/{categoryId:guid}", EndpointHandlers.UpdateCategory)
            .AddEndpointFilter<FilterUpdateCategory>()
            .Produces(200)
            .Produces(400)
            .Produces(404);
        
        shopGroupWithIds.MapDelete("/categories/{categoryId:guid}", EndpointHandlers.DeleteCategory)
            .AddEndpointFilter<FilterDeleteCategory>()
            .Produces(204)
            .Produces(404);

        shopGroupWithIds.MapGet("/categories/all", EndpointHandlers.GetCategories);
        
        shopGroupWithIds.MapGet("/categories/{categoryId:guid}", EndpointHandlers.GetCategory)
            .Produces<CategoryDto>(200)
            .Produces(404);
        
        // Products
        shopGroupWithIds.MapGet("/products", Products.EndpointHandler.GetProducts)
            .AddEndpointFilter<FilterGetProducts>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
        
        return shopGroup;
    }
}
