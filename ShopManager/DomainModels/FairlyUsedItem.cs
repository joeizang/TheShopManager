using NodaTime;

namespace ShopManager.DomainModels;

public class FairlyUsedItem : BaseDomainModel
{
    public Guid PurchaseId { get; set; }

    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; } = default!;

    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public ZonedDateTime DateBought { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = default!;

    public FairlyUsedItemCondition Condition { get; set; }

    public Guid ShopId { get; set; }

    public Shop Shop { get; set; } = default!;

    public string ItemName { get; set; } = string.Empty;

    public string ItemDescription { get; set; } = string.Empty;

    public string ItemDetails { get; set; } = string.Empty;

    public Money Price { get; set; } = new(Currency.NGN, 0m);

}