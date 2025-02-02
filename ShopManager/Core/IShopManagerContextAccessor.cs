using System;

namespace ShopManager.Core;

public interface IShopManagerContextAccessor
{
    Guid ShopId { get; }
    void SetShopId(Guid shopId);
}

public sealed class ShopManagerContextAccessor : IShopManagerContextAccessor
{
    public Guid ShopId { get; set; }

    public ShopManagerContextAccessor(Guid shopId)
    {
        ShopId = shopId;
    }

    public void SetShopId(Guid shopId)
    {
        ShopId = shopId;
    }
}
