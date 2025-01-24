using NodaTime;
using NodaTime.Extensions;
using NodaTime.Text;
using ShopManager.DomainModels;

namespace ShopManager.Core;

public static class CoreExtensions
{
    public static Instant ToInstantDate(this string date)
    {
        if (InstantPattern.ExtendedIso.Parse(date) is not { Success: true, Value: var parsedEndDate })
        {
            Guid.TryParse(Guid.Empty.ToString(), out var guid);
            return new Instant();
        }
        else
        {
            return parsedEndDate;
        }
    }
    
    public static bool TryParseStringToInstant(this string date, out Instant instant)
    {
        if (InstantPattern.ExtendedIso.Parse(date) is not { Success: true, Value: var parsedEndDate })
        {
            instant = DateTimeOffset.MinValue.ToInstant();
            return false;
        }
        else
        {
            instant = parsedEndDate;
            return true;
        }
    }
    
    public static Money ToDefaultMoneyValue(this Money money, Currency currency)
    {
        return new Money(currency, 0m);
    }
}