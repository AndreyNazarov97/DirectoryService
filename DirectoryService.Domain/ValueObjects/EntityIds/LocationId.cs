using DirectoryService.Domain.Abstractions;

namespace DirectoryService.Domain.ValueObjects.EntityIds;

public record LocationId : IEntityId
{
    public Guid Id { get; }
    private LocationId(Guid id) => Id = id;

    public static LocationId NewId() => new(Guid.NewGuid());
    public static LocationId Empty() => new(Guid.Empty);
    public static LocationId Create(Guid id) => new(id);

    public static implicit operator Guid(LocationId id) => id.Id;
}