using ShopManager.Features.Shops.Categories;
using ShopManager.Features.Shops.DomainModels;

namespace ShopManager.Features.Shops.Extensions;

public static class ShopExtensions
{
    public static Category MapToCategory(this CreateCategoryDto createCategoryDto)
    {
        return new Category
        {
            CategoryName = createCategoryDto.Name,
            CategoryDescription = createCategoryDto.Description,
            ShopId = createCategoryDto.ShopId
        };
    }
    
    public static CategoryDto MapToCategoryDto(this Category category)
    {
        return 
            new CategoryDto(category.CategoryName, category.CategoryDescription, 
                category.CategoryId, category.ShopId, category.CreatedAt.ToString());
    }
    
    public static CategoryDto ProjectToCategoryDto(this Category category)
    {
        return new CategoryDto(category.CategoryName, category.CategoryDescription, 
            category.CategoryId, category.ShopId, category.CreatedAt.ToString());
    }
    
    public static Category UpdateCategory(this Category category, CreateCategoryDto dto)
    {
        category.CategoryName = dto.Name;
        category.CategoryDescription = dto.Description;
        return category;
    }
}