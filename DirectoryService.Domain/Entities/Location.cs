using DirectoryService.Domain.Abstractions;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.EntityIds;
using TimeZone = DirectoryService.Domain.ValueObjects.TimeZone;

namespace DirectoryService.Domain.Entities;

public class Location : SoftDeletableEntity<LocationId>
{
    private readonly List<Address> _addresses = [];
    private readonly List<Department> _departments = [];
    
    public Location(
        LocationId id, 
        LocationName name,
        TimeZone timeZone
    ) : base(id)
    {
        Name = name;
        TimeZone = timeZone;
    }

    public LocationName Name { get; private set; }
    public TimeZone TimeZone { get; private set; }
    
    public IReadOnlyList<Address> Addresses => _addresses.AsReadOnly();
    public IReadOnlyList<Department> Departments => _departments.AsReadOnly();
}