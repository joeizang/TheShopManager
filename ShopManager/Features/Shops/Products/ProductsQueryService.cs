using Microsoft.EntityFrameworkCore;
using NodaTime;
using ShopManager.Data;

namespace ShopManager.Features.Shops.Products;

public class ProductsQueryService
{
    public static readonly Func<ShopManagerBaseContext, Guid, Instant, CancellationToken, Task<ProductsDto[]>>
        GetProducts = EF.CompileQuery((ShopManagerBaseContext context, Guid shopId,
                Instant cursor, CancellationToken token) =>
            context.Products.AsNoTracking()
                .Include(p => p.Categories)
                .Where(x => x.ShopId == shopId)
                .Where(x => x.CreatedAt > cursor)
                .OrderByDescending(x => x.Id)
                .ThenBy(x => x.CreatedAt)
                .Select(p => new ProductsDto( p.ProductName, p.SellingPrice.Amount,
                    p.ProductDescription, p.Id, p.ShopId, p.Categories.Select(x => x.Id).ToArray()))
                .AsSplitQuery()
                .Take(10)
                .ToArrayAsync(token));
}
