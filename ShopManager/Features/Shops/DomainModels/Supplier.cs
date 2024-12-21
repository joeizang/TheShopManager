using ShopManager.DomainModels;

namespace ShopManager.Features.Shops.DomainModels;

public class Supplier : BaseDomainModel
{
    public string SupplierName { get; set; } = string.Empty;

    public string SupplierAddress { get; set; } = string.Empty;

    public string SupplierPhoneNumber { get; set; } = string.Empty;

    public string SupplierEmailAddress { get; set; } = string.Empty;

    public Guid ShopId { get; set; }

    public Shop Shop { get; set; } = default!;

    public List<Product> Products { get; set; } = new();
}