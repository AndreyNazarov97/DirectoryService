using DirectoryService.Domain.Abstractions;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.EntityIds;

namespace DirectoryService.Domain.Entities;

public class Position : SoftDeletableEntity<PositionId>
{
    public PositionName Name { get; private set; }
    public Description Description { get; private set; }

    protected Position(
        PositionId id, 
        PositionName name, 
        Description description
        ) : base(id)
    {
        Name = name;
        Description = description;
    }
}