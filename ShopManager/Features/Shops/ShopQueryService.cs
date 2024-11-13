using System;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Extensions;
using ShopManager.Data;

namespace ShopManager.Features.Shops;

public class ShopQueryService
{
    public static readonly Func<ShopManagerBaseContext, Instant, IAsyncEnumerable<ShopDto>>
        GetShops = EF.CompileAsyncQuery((ShopManagerBaseContext context, Instant cursor) =>
            context.Shops.AsNoTracking()
                .Where(x => x.CreatedAt < cursor)
                .OrderByDescending(x => x.CreatedAt)
                .Select(s => new ShopDto(s.ShopName, s.ShopPhoneNumber, s.Status, s.ShopAddress, s.Id, s.CreatedAt))
                .Take(10));
    
    public static readonly Func<ShopManagerBaseContext, IAsyncEnumerable<ShopDto>>
        GetAllShops = EF.CompileAsyncQuery((ShopManagerBaseContext db) => 
            db.Shops.AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Select(s => new ShopDto(s.ShopName, s.ShopPhoneNumber, s.Status, s.ShopAddress, s.Id, s.CreatedAt))
                .Take(10));
    
    public static readonly Func<ShopManagerBaseContext, Guid, ShopDto?>
        GetShopById = EF.CompileQuery((ShopManagerBaseContext db, Guid shopId) => 
            db.Shops.AsNoTracking()
                .Where(x => x.Id == shopId)
                .Select(s => new ShopDto(s.ShopName, s.ShopPhoneNumber, s.Status, s.ShopAddress, s.Id, s.CreatedAt))
                .SingleOrDefault());
}
