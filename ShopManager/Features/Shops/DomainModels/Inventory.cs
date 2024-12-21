using ShopManager.DomainModels;

namespace ShopManager.Features.Shops.DomainModels;

public class Inventory : BaseDomainModel
{
    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public double QuantityInStock { get; set; }

    public double ReOrderLevel { get; set; }

    public double ReOrderQuantity { get; set; }

    public Guid ShopId { get; set; }

    public Shop Shop { get; set; } = default!;
}