using ShopManager.DomainModels;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Shops.Products;

public static class ProductsExtensions
{
    public static Product MapToProduct(this AddProductDto dto)
    {
        return new Product
        {
            ProductName = dto.ProductName,
            SellingPrice = new Money(Currency.NGN, dto.SellingPrice),
            ProductDescription = dto.ProductDescription,
            ShopId = dto.ShopId,
            Supplier = new Supplier
            {
                SupplierName = dto.SupplierName,
                SupplierAddress = dto.SupplierAddress,
                SupplierPhoneNumber = dto.SupplierPhoneNumber,
                SupplierEmailAddress = dto.SupplierEmailAddress,
                ShopId = dto.ShopId
            },
            Sku = dto.Sku,
            CostPrice = new Money(Currency.NGN, dto.CostPrice),
            IsFairlyUsed = dto.IsFairlyUsed,
            FairlyUsedItemId = dto.FairlyUsedItemId,
            Categories = dto.CategoryIds.Select(x => new Category { Id = x }).ToList()
        };
    }
    
    public static ProductsDto MapToProductsDto(this Product product)
    {
        return new ProductsDto(
            product.ProductName, product.SellingPrice.Amount, product.ProductDescription, 
            product.CreatedAt.ToDateTimeUtc().ToLongDateString(), product.Id,
            product.ShopId, product.Categories.Select(x => x.Id).ToArray());
    }
    
}