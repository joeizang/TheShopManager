using NodaTime;
using NodaTime.Text;

namespace ShopManager.Core;

public static class CoreExtensions
{
    public static Instant ToInstantDate(this string date)
    {
        if (InstantPattern.ExtendedIso.Parse(date) is not { Success: true, Value: var parsedEndDate })
        {
            return new Instant();
        }
        else
        {
            return parsedEndDate;
        }
    }
}