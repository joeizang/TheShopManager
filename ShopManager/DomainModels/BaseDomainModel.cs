using NodaTime;

namespace ShopManager.DomainModels;

public class BaseDomainModel
{
    public Guid Id { get; set; }

    public Instant CreatedAt { get; set; }

    public Instant UpdatedAt { get; set; }

    public BaseDomainModel()
    {
        Id = Ulid.NewUlid().ToGuid();
        CreatedAt = SystemClock.Instance.GetCurrentInstant();
        UpdatedAt = CreatedAt;
    }
}
