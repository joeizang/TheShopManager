using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Shops.Sales.DomainModels;

public class SaleItem : BaseDomainModel
{
    public Guid SaleId { get; set; }

    public Sale Sale { get; set; } = default!;

    public Guid ShopId { get; set; }

    public Shop Shop { get; set; } = default!;

    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public double QuantitySold { get; set; }

    public Money UnitPrice { get; set; } = new(Currency.NGN, 0m);

    public Money TotalAmount { get; set; } = new(Currency.NGN, 0m);

}