using NodaTime;

namespace ShopManager.DomainModels;

public class BaseDomainModel
{
    public Guid Id { get; set; }

    public ZonedDateTime CreatedAt { get; set; }

    public ZonedDateTime UpdatedAt { get; set; }

    public BaseDomainModel()
    {
        Id = Ulid.NewUlid().ToGuid();
        CreatedAt = SystemClock.Instance.GetCurrentInstant().InUtc();
        UpdatedAt = CreatedAt;
    }
}
