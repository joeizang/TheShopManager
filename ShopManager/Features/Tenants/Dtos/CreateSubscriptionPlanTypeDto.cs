namespace ShopManager.Features.Tenants;

public record CreateSubscriptionPlanTypeDto(string Name, string Description, decimal Price,
    string Features, decimal Discount);