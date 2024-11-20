using NodaTime;

namespace ShopManager.DomainModels;

public class Sale : BaseDomainModel
{
    public Guid SaleId { get; set; }

    public ZonedDateTime SaleDate { get; set; }

    public Money TotalAmount { get; set; } = new(Currency.NGN, 0m);

    public Guid? CustomerId { get; set; }

    public Customer Customer { get; set; } = default!;

    public string SalesPersonId { get; set; } = string.Empty;

    public ApplicationUser SalesPerson { get; set; } = default!;

    public Guid ShopId { get; set; }

    public Shop Shop { get; set; } = default!;

    public List<SaleItem> SaleItems { get; set; } = new();

    public List<Payment> Payments { get; set; } = new();
}