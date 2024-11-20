using FluentValidation;

namespace ShopManager.Features.Shops.Validations;

public class ValidateDeleteShop : AbstractValidator<DeleteShopDto>
{
    public ValidateDeleteShop()
    {
        RuleFor(x => x.ShopId)
            .NotEmpty().WithMessage("Shop Id is required");
    }
}