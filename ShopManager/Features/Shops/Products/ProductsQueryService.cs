using Microsoft.EntityFrameworkCore;
using ShopManager.Data;

namespace ShopManager.Features.Shops.Products;

public class ProductsQueryService
{
    public static readonly Func<ShopManagerBaseContext, Guid, IEnumerable<object>>
        GetProducts = EF.CompileQuery((ShopManagerBaseContext context, Guid shopId) =>
            context.Products.AsNoTracking()
                .Include(x => x.Supplier)
                .Include(x => x.Shop)
                .Include(x => x.Categories.Where(c => c.ShopId == shopId))
                .Where(x => x.ShopId == shopId)
                .OrderByDescending(x => x.CreatedAt)
                .ThenBy(x => x.Id)
                .Select(p => new { p.ProductName, p.SellingPrice, p.ProductDescription, p.Id })
                .AsSplitQuery()
                .Take(10));
}
