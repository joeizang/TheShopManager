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
                .Select(s => new ShopDto(s.ShopName, s.ShopPhoneNumber, s.Status, s.ShopAddress, s.Id))
                .Take(10));
}
