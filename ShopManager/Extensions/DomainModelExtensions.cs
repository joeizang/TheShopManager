using System;
using ShopManager.DomainModels;

namespace ShopManager.Extensions;

public static class DomainModelExtensions
{
    public static Money ToDefaultMoneyValue(this Money money, Currency currency)
    {
        return new Money(currency, 0m);
    }
}
