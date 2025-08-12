using DirectoryService.Domain.Abstractions;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.EntityIds;
using Path = DirectoryService.Domain.ValueObjects.Path;

namespace DirectoryService.Domain.Entities;

public class Department : AggregateRoot<DepartmentId>
{
    private readonly List<Position> _positions = [];
    private readonly List<Location> _locations = [];
    
    public Department(
        DepartmentId id, 
        DepartmentName name, 
        Identifier identifier, 
        DepartmentId? parentId, 
        Path path, 
        Depth depth, 
        ChildrenCount childrenCount
        ) : base(id)
    {
        Name = name;
        Identifier = identifier;
        ParentId = parentId;
        Path = path;
        Depth = depth;
        ChildrenCount = childrenCount;
    }
    
    public DepartmentName Name { get; private set; }
    public Identifier Identifier { get; private set; }
    public DepartmentId? ParentId { get; private set; }
    public Path Path { get; private set; }
    public Depth Depth { get; private set; }
    public ChildrenCount ChildrenCount { get; private set; }
    
    public IReadOnlyList<Position> Positions => _positions.AsReadOnly();
    public IReadOnlyList<Location> Locations => _locations.AsReadOnly();
    
}