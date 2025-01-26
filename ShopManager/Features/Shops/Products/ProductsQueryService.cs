using Microsoft.EntityFrameworkCore;
using NodaTime;
using ShopManager.Data;

namespace ShopManager.Features.Shops.Products;

public class ProductsQueryService
{
    public static readonly Func<ShopManagerBaseContext, Guid, Instant, CancellationToken, Task<ProductsDto[]>>
        GetProducts = EF.CompileQuery((ShopManagerBaseContext context, Guid shopId,
                Instant cursor, CancellationToken token) =>
            context
                .Products
                .AsNoTracking()
                .Include(p => p.Categories)
                .Where(x => x.ShopId == shopId)
                .Where(x => x.CreatedAt > cursor)
                .OrderByDescending(x => x.Id)
                .ThenBy(x => x.CreatedAt)
                .Select(p => new ProductsDto( 
                    p.ProductName, 
                    p.SellingPrice.Amount,
                    p.ProductDescription, 
                    p.CreatedAt.ToDateTimeUtc().ToLongDateString(),
                    p.Id,
                    p.ShopId, 
                    p.Categories.Select(x => x.Id).ToArray()))
                .AsSplitQuery()
                .Take(10)
                .ToArrayAsync(token));
    
    public static readonly Func<ShopManagerBaseContext, Guid, CancellationToken, Task<ProductsDto[]>>
        GetAllProducts = EF.CompileQuery(
            (ShopManagerBaseContext context, Guid shopId, CancellationToken token) =>
                context
                .Products
                .AsNoTracking()
                .Where(p => p.ShopId == shopId)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProductsDto(
                    p.ProductName, 
                    p.SellingPrice.Amount,
                    p.ProductDescription, 
                    p.CreatedAt.ToDateTimeUtc().ToLongDateString(),
                    p.Id,
                    p.ShopId, 
                    p.Categories.Select(x => x.Id).ToArray()))
                .AsSplitQuery()
                .Take(10)
                .ToArrayAsync(token));
    
    public static readonly Func<ShopManagerBaseContext, string, Guid, CancellationToken, Task<ProductsDto[]>>
        GetProductsByName = EF.CompileQuery(
            (ShopManagerBaseContext context, string name, Guid shopId, CancellationToken token) =>
                context.Products
                .AsNoTracking()
                .Where(p => p.ShopId == shopId)
                .Where(p => p.ProductName.Contains(name))
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProductsDto(
                    p.ProductName, 
                    p.SellingPrice.Amount,
                    p.ProductDescription, 
                    p.CreatedAt.ToDateTimeUtc().ToLongDateString(),
                    p.Id,
                    p.ShopId, 
                    p.Categories.Select(x => x.Id).ToArray()))
                .AsSplitQuery()
                .Take(10)
                .ToArrayAsync(token));
    
    public static readonly Func<ShopManagerBaseContext, Guid, Guid, CancellationToken, Task<ProductsDto[]>>
        GetProductByCategory = EF.CompileQuery(
            (ShopManagerBaseContext context, Guid shopId, Guid categoryId, CancellationToken token) =>
                context.Products
                .AsNoTracking()
                .Where(p => p.ShopId == shopId)
                .Where(p => p.Categories.Any(x => x.Id == categoryId))
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProductsDto(
                    p.ProductName, 
                    p.SellingPrice.Amount,
                    p.ProductDescription, 
                    p.CreatedAt.ToDateTimeUtc().ToLongDateString(),
                    p.Id,
                    p.ShopId, Array.Empty<Guid>()))
                .AsSplitQuery()
                .ToArrayAsync(token));
    
    public static readonly Func<ShopManagerBaseContext, Guid, Guid, CancellationToken, Task<ProductsDto?>>
        GetProductById = EF.CompileQuery(
            (ShopManagerBaseContext context, Guid shopId, Guid productId, CancellationToken token) =>
                context.Products
                .AsNoTracking()
                .Include(p => p.Categories)
                .Where(p => p.ShopId == shopId)
                .Where(p => p.Id == productId)
                .Select(p => new ProductsDto(
                    p.ProductName, 
                    p.SellingPrice.Amount,
                    p.ProductDescription, 
                    p.CreatedAt.ToDateTimeUtc().ToLongDateString(),
                    p.Id,
                    p.ShopId, p.Categories.Select(x => x.Id).ToArray()))
                .AsSplitQuery()
                .FirstOrDefaultAsync(token));
}
