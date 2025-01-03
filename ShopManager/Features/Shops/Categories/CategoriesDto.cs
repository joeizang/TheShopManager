namespace ShopManager.Features.Shops.Categories;

public record CategoryDto(string CategoryName, string CategoryDescription, Guid CategoryId, 
    Guid ShopId, string CreatedAt);

public record CreateCategoryDto(string Name, string Description, Guid ShopId);
