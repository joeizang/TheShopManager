using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Features.Shops.Abstractions;
using ShopManager.Features.Shops.Categories;
using ShopManager.Features.Shops.Extensions;
namespace ShopManager.Features.Shops.Services;

public class CategoryCommandService(ShopManagerBaseContext context) : ICategoryCommandService
{
    public async Task<Option<CategoryDto>> CreateCategoryAsync(CreateCategoryDto dto, 
        CancellationToken cancellationToken)
    {
        var category = dto.MapToCategory();
        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var result = await context.Categories
            .FindAsync(category.Id, cancellationToken).ConfigureAwait(false);
        return result is null ? Option<CategoryDto>.None : Option<CategoryDto>.Some(result.MapToCategoryDto());
    }

    public async Task<Result<CategoryDto>> UpdateCategoryAsync(int categoryId, CreateCategoryDto dto, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}