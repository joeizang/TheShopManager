namespace ShopManager.DomainModels;

public class Category : BaseDomainModel
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string CategoryDescription { get; set; } = string.Empty;

    public Guid ShopId { get; set; }

    public Shop Shop { get; set; } = default!;
}