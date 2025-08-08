using DirectoryService.Domain.Abstractions;

namespace DirectoryService.Domain.ValueObjects.EntityIds;

public record PositionId : IEntityId
{
    public Guid Id { get; }
    private PositionId(Guid id) => Id = id;

    public static PositionId NewId() => new(Guid.NewGuid());
    public static PositionId Empty() => new(Guid.Empty);
    public static PositionId Create(Guid id) => new(id);

    public static implicit operator Guid(PositionId id) => id.Id;
}