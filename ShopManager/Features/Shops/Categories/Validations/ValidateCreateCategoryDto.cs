using FluentValidation;

namespace ShopManager.Features.Shops.Categories.Validations;

public class ValidateCreateCategoryDto : AbstractValidator<CreateCategoryDto>
{
    public ValidateCreateCategoryDto()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category name is required");

        RuleFor(x => x.ShopId)
            .NotEmpty()
            .WithMessage("Shop Id is required");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Category Description would be nice");
    }
}