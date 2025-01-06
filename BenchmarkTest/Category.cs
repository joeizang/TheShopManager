using System;
using System.Collections.Generic;

namespace BenchmarkTest;

public partial class Category
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryDescription { get; set; } = null!;

    public Guid ShopId { get; set; }

    public Guid? ProductId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Shop Shop { get; set; } = null!;
}
