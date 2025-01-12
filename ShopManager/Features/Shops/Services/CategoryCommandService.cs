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

    public async Task<Option<CategoryDto>> UpdateCategoryAsync(Guid shopId, Guid categoryId, CreateCategoryDto dto,
        CancellationToken cancellationToken)
    {
        var category = await context.Categories.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == categoryId && x.ShopId == shopId, cancellationToken)
            .ConfigureAwait(false);
        if(category is null)
            return Option<CategoryDto>.None;
        category.UpdateCategory(dto);
        context.Entry(category).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return Option<CategoryDto>.Some(category.MapToCategoryDto());
    }

    public async Task<Option<IResult>> DeleteCategoryAsync(Guid shopId, Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await context.Categories.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == categoryId && x.ShopId == shopId, cancellationToken)
            .ConfigureAwait(false);
        if(category is null)
            return Option<IResult>.None;
        category.IsDeleted = true;
        context.Entry(category).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return Option<IResult>.Some(TypedResults.NoContent());
    }
}