using System;
using ShopManager.Features.Shops.Categories.Filters;

namespace ShopManager.Features.Shops.Categories;

public static class CategoryEndpoints
{
    public static void AddCategoryEndpoints(RouteGroupBuilder shopGroupWithIds)
    {
        shopGroupWithIds.MapPost("/categories", EndpointHandlers.CreateCategory)
            .AddEndpointFilter<FilterCreateCategory>()
            .Produces<CategoryDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        shopGroupWithIds.MapPut("/categories/{categoryId:guid}", EndpointHandlers.UpdateCategory)
            .AddEndpointFilter<FilterUpdateCategory>()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
        
        shopGroupWithIds.MapDelete("/categories/{categoryId:guid}", EndpointHandlers.DeleteCategory)
            .AddEndpointFilter<FilterDeleteCategory>()
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        shopGroupWithIds.MapGet("/categories/all", EndpointHandlers.GetCategories);
        
        shopGroupWithIds.MapGet("/categories/{categoryId:guid}", EndpointHandlers.GetCategory)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}
