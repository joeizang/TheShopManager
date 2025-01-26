namespace ShopManager.Features.Shops.Products;

public record ProductsDto(string ProductName, decimal SellingPrice, string ProductDescription, string CreatedAt,
    Guid ProductId, Guid ShopId, Guid[] CategoryIds);

public record AddProductDto(string ProductName, decimal SellingPrice, string ProductDescription, Guid ShopId,
string SupplierName,string SupplierAddress, string SupplierPhoneNumber, string Sku, decimal CostPrice,
string SupplierEmailAddress, bool IsFairlyUsed, Guid? FairlyUsedItemId, List<Guid> CategoryIds);
