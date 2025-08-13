using CSharpFunctionalExtensions;
using DirectoryService.Domain.Abstractions;
using MyNugets.Errors;

namespace DirectoryService.Domain.ValueObjects;

public record Path: IValueObject<string>
{
    public string Value { get; }
    
    private Path(string value) => Value = value;

    public static Result<Path, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid(nameof(Path));
        
        
        return new Path(name);
    }
    
}