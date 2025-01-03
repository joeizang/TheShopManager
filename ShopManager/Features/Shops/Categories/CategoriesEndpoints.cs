using ShopManager.Core;
using ShopManager.Features.Shops.Categories.Filters;

namespace ShopManager.Features.Shops.Categories;

public static class CategoriesEndpoints
{
    public static RouteGroupBuilder MapCategoriesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup($"{Constants.V1}/shops/categories");
        var groupWithId = group.MapGroup("/{categoryId:guid}");
        
        group.MapPost("", EndpointHandlers.CreateCategory)
            .AddEndpointFilter<FilterCreateCategory>()
            .Produces<CategoryDto>(201)
            .Produces(400)
            .Produces(500);

        return group;
    }
}
