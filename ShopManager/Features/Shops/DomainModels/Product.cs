using ShopManager.DomainModels;
using ShopManager.Features.Shops.Products;

namespace ShopManager.Features.Shops.DomainModels;

public class Product : BaseDomainModel
    {
        public Guid SupplierId { get; set; }

        public Supplier Supplier { get; set; } = new();

        public string Sku { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public Money CostPrice { get; set; } = new(Currency.NGN, 0m);

        public Money SellingPrice { get; set; } = new(Currency.NGN, 0m);

        public bool IsFairlyUsed { get; set; }

        public List<Category> Categories { get; set; } = [];

        public Guid? FairlyUsedItemId { get; set; }

        public FairlyUsedItem? FairlyUsedItem { get; set; }

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; } = default!;
        
        public void UpdateProduct(AddProductDto dto)
        {
            ProductName = dto.ProductName;
            SellingPrice = new Money(Currency.NGN, dto.SellingPrice);
            ProductDescription = dto.ProductDescription;
            Sku = dto.Sku;
            CostPrice = new Money(Currency.NGN, dto.CostPrice);
            IsFairlyUsed = dto.IsFairlyUsed;
            FairlyUsedItemId = dto.FairlyUsedItemId;
            Categories = dto.CategoryIds.Select(x => new Category { Id = x }).ToList();
        }
    }