using CSharpFunctionalExtensions;
using DirectoryService.Domain.Abstractions;
using DirectoryService.Domain.Constants;
using MyNugets.Errors;

namespace DirectoryService.Domain.ValueObjects;

public record LocationName: IValueObject<string>
{
    public string Value { get; }
    
    private LocationName(string value) => Value = value;

    public static Result<LocationName, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid(nameof(LocationName));
        
        if(name.Length is > LocationNameConstants.MAX_LENGTH or < LocationNameConstants.MIN_LENGTH)
            return Errors.General.LengthIsInvalid(nameof(LocationName));
        
        return new LocationName(name);
    }
    
}