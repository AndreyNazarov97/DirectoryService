using CSharpFunctionalExtensions;
using DirectoryService.Domain.Abstractions;
using MyNugets.Errors;

namespace DirectoryService.Domain.ValueObjects;

public record Depth: IValueObject<short>
{
    public short Value { get; }
    
    private Depth(short value) => Value = value;

    public static Result<Depth, Error> Create(short depth)
    {
        if (depth < 0)
        {
            return Errors.General.ValueIsInvalid(nameof(Depth));
        }
        
        
        return new Depth(depth);
    }
    
}