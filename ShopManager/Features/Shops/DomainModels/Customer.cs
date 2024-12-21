using ShopManager.DomainModels;
using ShopManager.Features.Shops.Sales.DomainModels;

namespace ShopManager.Features.Shops.DomainModels;

public class Customer : BaseDomainModel
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public List<Sale> Sales { get; set; } = new();

    public Guid ShopId { get; set; }

    public Shop Shop { get; set; } = default!;
}