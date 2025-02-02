using NodaTime;
using ShopManager.Features.Shops.Sales;
using Xunit;

namespace ShopManager.Tests.ExtensionMethodTests;

[Collection("StringConversion Tests")]
public class StringConversionTests
{
    [Fact]
    public void ConvertStringToZonedDateTime()
    {
        var dateTimeString = "2024-01-01T00:00:00Z";
        var zonedDateTime = dateTimeString.ConvertToZonedDateTime();
        Assert.Equal(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero), zonedDateTime.ToDateTimeOffset());
    }

    [Fact]
    public void ConvertStringToZonedDateTime_InvalidFormat()
    {
        var dateTimeString = "testing";

        ZonedDateTime LocaFunction(string testString)
        {
            return testString.ConvertToZonedDateTime();
        }
        Assert.Throws<FormatException>(() => LocaFunction(dateTimeString));
    }

    [Fact]
    public void ConvertStringToZonedDateTime_InvalidDateTime()
    {
        var dateTimeString = "01/02/2024";
        var result = dateTimeString.ConvertToZonedDateTime();
        Assert.IsType<ZonedDateTime>(result);
    }



}
