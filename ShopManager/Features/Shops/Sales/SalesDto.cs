namespace ShopManager.Features.Shops.Sales;

public record SaleSummaryDto(Guid SaleId, string CreatedAt, decimal TotalAmount);

public record SaleDetailDto(Guid SaleId, string SalesPersonEmail, string CreatedAt, decimal TotalAmount,
    SaleItemDto[] SaleItems, double DiscountPercentage = 0, decimal DiscountAmount = 0, string CustomerName = ""
);

public record SaleItemDto(Guid ProductId, string ProductName, double Quantity, decimal UnitPrice);

public record MakeSaleDto(Guid ShopId, string SalesPersonEmail, string SaleDate, string CustomerName, SaleItemDto[] SaleItems, double DiscountPercentage = 0);

public record MakeSaleResultDto(Guid SaleId, string CreatedAt, decimal TotalAmount);