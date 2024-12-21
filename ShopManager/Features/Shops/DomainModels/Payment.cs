using NodaTime;
using ShopManager.DomainModels;
using ShopManager.Features.Shops.Sales.DomainModels;

namespace ShopManager.Features.Shops.DomainModels;

public class Payment : BaseDomainModel
{
    public Guid SalesId { get; set; }

    public Sale Sale { get; set; } = default!;

    public Guid ShopId { get; set; }

    public Shop Shop { get; set; } = default!;

    public Money AmountPaid { get; set; } = new(Currency.NGN, 0m);

    public Instant PaymentDate { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public string PaymentReference { get; set; } = string.Empty;

}