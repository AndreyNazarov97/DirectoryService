using CSharpFunctionalExtensions;
using DirectoryService.Domain.Abstractions;
using MyNugets.Errors;

namespace DirectoryService.Domain.ValueObjects;

public record Address: IValueObject<string>
{
    public string Value { get; }
    
    private Address(string value) => Value = value;

    public static Result<Address, Error> Create(string name)
    {
        if (string.IsNullOrEmpty(name))
            return Errors.General.ValueIsInvalid(nameof(Address));
        
        return new Address(name);
    }
}