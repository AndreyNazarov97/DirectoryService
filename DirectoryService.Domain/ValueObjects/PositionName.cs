using CSharpFunctionalExtensions;
using DirectoryService.Domain.Abstractions;
using DirectoryService.Domain.Constants;
using MyNugets.Errors;

namespace DirectoryService.Domain.ValueObjects;

public record PositionName: IValueObject<string>
{
    public string Value { get; }
    
    private PositionName(string value) => Value = value;

    public static Result<PositionName, Error> Create(string name)
    {
        if (string.IsNullOrEmpty(name))
            return Errors.General.ValueIsInvalid(nameof(PositionName));
        
        if(name.Length is > PositionNameConstants.MAX_LENGTH or < PositionNameConstants.MIN_LENGTH)
            return Errors.General.LengthIsInvalid(nameof(PositionName));
        
        return new PositionName(name);
    }
    
}