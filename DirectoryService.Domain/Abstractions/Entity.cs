namespace DirectoryService.Domain.Abstractions;

public abstract class Entity<TId> where TId : IEntityId
{
    public TId Id { get; }
    public DateTimeOffset? CreatedAt { get; protected set; }    
    public DateTimeOffset? UpdatedAt { get; protected set; }
    
    public void SetCreatedAt(DateTimeOffset createdAt) => CreatedAt = createdAt;
    public void SetUpdatedAt(DateTimeOffset updatedAt) => UpdatedAt = updatedAt;

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (Entity<TId>)obj;
        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return (GetType().FullName + Id).GetHashCode();
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            return true;

        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }
}