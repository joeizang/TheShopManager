using LanguageExt;
using LanguageExt.Common;
using ShopManager.Features.Shops.Categories;

namespace ShopManager.Features.Shops.Abstractions;

public interface ICategoryCommandService
{
    Task<Option<CategoryDto>> CreateCategoryAsync(CreateCategoryDto dto, CancellationToken cancellationToken);
    
    Task<Option<CategoryDto>> UpdateCategoryAsync(Guid shopId, Guid categoryId, CreateCategoryDto dto, CancellationToken cancellationToken);
    
    Task<Option<IResult>> DeleteCategoryAsync(Guid shopId, Guid categoryId, CancellationToken cancellationToken);
}