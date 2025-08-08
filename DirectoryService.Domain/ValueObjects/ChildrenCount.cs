using CSharpFunctionalExtensions;
using DirectoryService.Domain.Abstractions;
using MyNugets.Errors;

namespace DirectoryService.Domain.ValueObjects;

public record ChildrenCount: IValueObject<int>
{
    public int Value { get; }
    
    private ChildrenCount(int value) => Value = value;

    public static Result<ChildrenCount, Error> Create(int count)
    {
        if (count < 0)
        {
            return Errors.General.ValueIsInvalid(nameof(ChildrenCount));
        }
        
        return new ChildrenCount(count);
    }
    
}