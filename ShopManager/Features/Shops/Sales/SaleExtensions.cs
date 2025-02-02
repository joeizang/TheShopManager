using System;
using NodaTime;

namespace ShopManager.Features.Shops.Sales;

public static class SaleExtensions
{
    public static ZonedDateTime ConvertToZonedDateTime(this string dateTimeString)
    {
        var result =ZonedDateTime.FromDateTimeOffset(DateTimeOffset.Parse(dateTimeString));
        return result;
    }
}
