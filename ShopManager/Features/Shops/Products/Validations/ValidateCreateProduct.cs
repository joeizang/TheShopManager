using FluentValidation;

namespace ShopManager.Features.Shops.Products.Validations;

public class ValidateCreateProduct : AbstractValidator<AddProductDto>
{
    public ValidateCreateProduct()
    {
        RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product name is required");
        RuleFor(x => x.SellingPrice).GreaterThan(0).WithMessage("Selling price must be greater than 0");
        RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Product description is required");
        RuleFor(x => x.ShopId).NotEmpty().WithMessage("Shop id is required");
        RuleFor(x => x.SupplierName).NotEmpty().WithMessage("Supplier name is required");
        RuleFor(x => x.SupplierAddress).NotEmpty().WithMessage("Supplier address is required");
        RuleFor(x => x.SupplierPhoneNumber).NotEmpty().WithMessage("Supplier phone number is required");
        RuleFor(x => x.Sku).NotEmpty().WithMessage("Sku is required");
        RuleFor(x => x.CostPrice).GreaterThan(0).WithMessage("Cost price must be greater than 0");
        RuleFor(x => x.SupplierEmailAddress).NotEmpty().WithMessage("Supplier email address is required");
        RuleFor(x => x.CategoryIds).NotEmpty().WithMessage("Category ids are required");
    }
}