using System.Text.Json;
using CSharpFunctionalExtensions;
using DirectoryService.Domain.Abstractions;
using MyNugets.Errors;

namespace DirectoryService.Domain.ValueObjects;

public record Address : IValueObject<string>
{
    public string City { get; }

    public string Street { get; }

    public string HouseNumber { get; }

    public string? Number { get; }
    public string Value => $"{City} {Street} {HouseNumber} {Number}";

    public string ToJson() => JsonSerializer.Serialize(Value, JsonSerializerOptions.Default);

    private Address(
        string city,
        string street,
        string houseNumber,
        string? number)
    {
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        Number = number;
    }

    public static Result<Address, Error> Create(
        string city,
        string street,
        string houseNumber,
        string? number)
    {
        var errors = Validate(city, street, houseNumber, number);

        if (errors.Errors.Count > 0)
            return errors.Errors.First();

        return new Address(city, street, houseNumber, number);
    }

    private static ErrorList Validate(
        string city,
        string street,
        string houseNumber,
        string? number)
    {
        string[] values = number == null ? [city, street, houseNumber] : [city, street, houseNumber, number];

        var errors = new List<Error>();
        foreach (var value in values)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                errors.Add(Errors.General.ValueIsInvalid(nameof(Address))); 
            }
        }
        
        return errors;
    }
}