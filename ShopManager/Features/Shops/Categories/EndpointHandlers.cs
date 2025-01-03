using Microsoft.AspNetCore.Mvc;
using ShopManager.Features.Shops.Abstractions;

namespace ShopManager.Features.Shops.Categories;

public static class EndpointHandlers
{
    public static async Task<IResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto,
        [FromServices] ICategoryCommandService service, CancellationToken cancellationToken)
    {
        var result = await service.CreateCategoryAsync(createCategoryDto, cancellationToken)
            .ConfigureAwait(false);
        return result.Match<IResult>((r) =>
            TypedResults.Created<CategoryDto>("", r),
            () => TypedResults.InternalServerError("Failed to create category"));
    }

    public static Task<object?> GetCategories(Guid shopId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public static Task<object?> GetCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public static Task<object?> UpdateCategory(Guid categoryId, CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public static Task<object?> DeleteCategory(Guid categoryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
