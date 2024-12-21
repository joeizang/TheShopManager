namespace ShopManager.Features.Tenants.Dtos;

public record CreateSubscriptionPlanTypeDto(string Name, string Description, decimal Price,
    string Features, decimal Discount);