using DirectoryService.Domain.Abstractions;

namespace DirectoryService.Domain.ValueObjects.EntityIds;

public record DepartmentId : IEntityId
{
    public Guid Id { get; }
    private DepartmentId(Guid id) => Id = id;

    public static DepartmentId NewId() => new (Guid.NewGuid());
    public static DepartmentId Empty() => new (Guid.Empty);
    public static DepartmentId Create(Guid id) => new(id);

    public static implicit operator Guid(DepartmentId id) => id.Id;
}