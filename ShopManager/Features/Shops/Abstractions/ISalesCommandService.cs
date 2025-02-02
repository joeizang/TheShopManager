using System;
using LanguageExt;
using ShopManager.Data;
using ShopManager.Features.Shops.Sales;

namespace ShopManager.Features.Shops.Abstractions;

public interface ISalesCommandService
{
    Task<Option<SaleSummaryDto>> MakeSale(ShopManagerBaseContext context, Guid shopId, MakeSaleDto sale, CancellationToken token);
}
