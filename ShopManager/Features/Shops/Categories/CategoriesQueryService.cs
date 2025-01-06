using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Features.Shops.Extensions;

namespace ShopManager.Features.Shops.Categories;

public static class CategoriesQueryService
{
    public static readonly Func<ShopManagerBaseContext, Guid, IEnumerable<CategoryDto>> GetCategories =
        EF.CompileQuery((ShopManagerBaseContext context, Guid shopId) =>
            context.Categories.AsNoTracking()
                .Where(c => c.ShopId == shopId)
                .Select(c => c.ProjectToCategoryDto())
                .Take(10)
                .ToList());
    
    public static async Task<IEnumerable<CategoryDto>> GetAllCategories(Guid shopId, ShopManagerBaseContext context)
    {
        var results = await context.Categories.AsNoTracking()
            .Where(x => x.ShopId == shopId)
            .Select(x => x.ProjectToCategoryDto())
            .Take(10)
            .ToListAsync()
            .ConfigureAwait(false);
        return results;
    }
}
