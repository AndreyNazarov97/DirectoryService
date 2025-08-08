namespace DirectoryService.Domain.Abstractions;

public interface IValueObject<out TValue>
{
    public TValue Value { get; }
}