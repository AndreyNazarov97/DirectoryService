namespace DirectoryService.Domain.Abstractions;

public class AggregateRoot<TId> : SoftDeletableEntity<TId> where TId : IEntityId
{
    public AggregateRoot(TId id) : base(id)
    {
    }
}