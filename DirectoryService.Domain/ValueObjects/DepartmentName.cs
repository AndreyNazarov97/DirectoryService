using CSharpFunctionalExtensions;
using DirectoryService.Domain.Abstractions;
using DirectoryService.Domain.Constants;
using MyNugets.Errors;

namespace DirectoryService.Domain.ValueObjects;

public record DepartmentName : IValueObject<string>
{
    public string Value { get; }
    
    private DepartmentName(string value) => Value = value;

    public static Result<DepartmentName, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsInvalid(nameof(DepartmentName));
        
        if(name.Length is > DepartmentNameConstants.MAX_LENGTH or < DepartmentNameConstants.MIN_LENGTH)
            return Errors.General.LengthIsInvalid(nameof(DepartmentName));
        
        return new DepartmentName(name);
    }
}