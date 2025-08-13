using CSharpFunctionalExtensions;
using DirectoryService.Domain.Abstractions;
using DirectoryService.Domain.Constants;
using MyNugets.Errors;

namespace DirectoryService.Domain.ValueObjects;

public record TimeZone: IValueObject<string>
{
public string Value { get; }
    
private TimeZone(string value) => Value = value;

public static Result<TimeZone, Error> Create(string ianaId)
{
    if (string.IsNullOrEmpty(ianaId))
        return Errors.General.ValueIsInvalid(nameof(TimeZone));
    
    try
    {
      var timeZone =  TimeZoneInfo.FindSystemTimeZoneById(ianaId);
    }
    catch (TimeZoneNotFoundException)
    {
        return Error.Validation(
            code: "TimeZone.Invalid", 
            message: $"The value '{ianaId}' is not a valid IANA time zone identifier.");
    }
    catch (InvalidTimeZoneException)
    {
        return Error.Failure(
            code: "TimeZone.Corrupt", 
            message: $"The time zone data for '{ianaId}' on the host system is corrupt.");
    }
        
    return new TimeZone(ianaId);
}
    
}