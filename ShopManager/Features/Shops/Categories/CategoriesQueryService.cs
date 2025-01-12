using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Features.Shops.Extensions;

namespace ShopManager.Features.Shops.Categories;

public static class CategoriesQueryService
{
    public static readonly Func<ShopManagerBaseContext, Guid, CancellationToken, Task<CategoryDto[]>> GetCategories =
        EF.CompileQuery((ShopManagerBaseContext context, Guid shopId, CancellationToken token) =>
            context.Categories.AsNoTracking()
                .Where(c => c.ShopId == shopId)
                .Select(c => c.ProjectToCategoryDto())
                .Take(10)
                .ToArrayAsync(token));
    
    public static readonly Func<ShopManagerBaseContext, Guid, Guid, CancellationToken, Task<CategoryDto?>> GetCategory =
        EF.CompileQuery((ShopManagerBaseContext context, Guid shopId, Guid categoryId, CancellationToken token) =>
            context.Categories.AsNoTracking()
                .Where(c => c.ShopId == shopId && c.Id == categoryId)
                .Select(c => c.ProjectToCategoryDto())
                .SingleOrDefaultAsync(token));
}
