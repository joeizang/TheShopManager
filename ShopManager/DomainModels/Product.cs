namespace ShopManager.DomainModels;

using Microsoft.EntityFrameworkCore;
using ShopManager.Extensions;
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

        public Guid CategoryId { get; set; }

        public List<Category> Categories { get; set; } = new();

        public Guid? FairlyUsedItemId { get; set; }

        public FairlyUsedItem? FairlyUsedItem { get; set; }

        public Guid ShopId { get; set; }

        public Shop Shop { get; set; } = new();
    }