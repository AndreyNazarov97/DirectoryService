using CSharpFunctionalExtensions;
using DirectoryService.Domain.Abstractions;
using DirectoryService.Domain.Constants;
using MyNugets.Errors;

namespace DirectoryService.Domain.ValueObjects;

public record Description: IValueObject<string>
{
    public string Value { get; }
    
    private Description(string value) => Value = value;

    public static Result<Description, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid(nameof(Description));
        
        if(name.Length is > PositionNameConstants.MAX_LENGTH)
            return Errors.General.LengthIsInvalid(nameof(Description));
        
        return new Description(name);
    }
    
}