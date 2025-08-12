using DirectoryService.Domain.Abstractions;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.EntityIds;

namespace DirectoryService.Domain.Entities;

public class Position : SoftDeletableEntity<PositionId>
{
    private readonly List<Department> _departments = [];

    public Position(
        PositionId id,
        PositionName name,
        Description? description = null
    ) : base(id)
    {
        Name = name;
        Description = description;
    }

    public PositionName Name { get; private set; }
    public Description? Description { get; private set; }


    public IReadOnlyList<Department> Departments => _departments.AsReadOnly();
}