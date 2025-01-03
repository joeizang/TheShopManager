using LanguageExt;
using LanguageExt.Common;
using ShopManager.Features.Shops.Categories;

namespace ShopManager.Features.Shops.Abstractions;

public interface ICategoryCommandService
{
    Task<Option<CategoryDto>> CreateCategoryAsync(CreateCategoryDto dto, CancellationToken cancellationToken);
    
    Task<Result<CategoryDto>> UpdateCategoryAsync(int categoryId, CreateCategoryDto dto, CancellationToken cancellationToken);
    
    Task DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken);
}