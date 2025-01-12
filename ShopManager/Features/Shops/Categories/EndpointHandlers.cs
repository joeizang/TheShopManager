using Microsoft.AspNetCore.Mvc;
using ShopManager.Data;
using ShopManager.Features.Shops.Abstractions;

namespace ShopManager.Features.Shops.Categories;

public static class  EndpointHandlers
{
    public static async Task<IResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto,
        [FromServices] ICategoryCommandService service, CancellationToken cancellationToken)
    {
        var result = await service.CreateCategoryAsync(createCategoryDto, cancellationToken)
            .ConfigureAwait(false);
        return result.Match<IResult>((r) =>
            TypedResults.Created("", r),
            () => TypedResults.InternalServerError("Failed to create category"));
    }

    public static async Task<IResult> GetCategories(Guid shopId,
        [FromServices] ShopManagerBaseContext context, CancellationToken cancellationToken)
    {
        var result = await CategoriesQueryService.GetCategories(context, shopId, cancellationToken)
            .ConfigureAwait(false);
        return TypedResults.Ok(result);
    }

    public static IResult GetCategory(Guid shopId, Guid categoryId, 
        [FromServices] ShopManagerBaseContext context, CancellationToken token)
    {
        var result = CategoriesQueryService.GetCategory(context, shopId, categoryId, token);
        return TypedResults.Ok(result);
    }

    public static async Task<IResult> UpdateCategory(Guid shopId, Guid categoryId, [FromBody] CreateCategoryDto createCategoryDto, 
       [FromServices] ICategoryCommandService service, CancellationToken cancellationToken)
    {
        var result = await service.UpdateCategoryAsync(shopId, 
                categoryId, createCategoryDto, cancellationToken)
            .ConfigureAwait(false);
        return result.Match<IResult>(TypedResults.Ok,
            () => TypedResults.BadRequest("Category not found"));
    }

    public static async Task<IResult> DeleteCategory(Guid shopId, Guid categoryId,
        [FromServices] ICategoryCommandService service, CancellationToken cancellationToken)
    {
        var result = await service.DeleteCategoryAsync(shopId,categoryId, cancellationToken)
            .ConfigureAwait(false);
        return result.Match<IResult>((r) => r,
            () => TypedResults.NotFound("Category not found"));
    }
}
