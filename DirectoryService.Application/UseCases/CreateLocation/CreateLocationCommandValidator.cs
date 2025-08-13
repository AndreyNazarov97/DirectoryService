using DirectoryService.Application.Validation;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;
using TimeZone = DirectoryService.Domain.ValueObjects.TimeZone;

namespace DirectoryService.Application.UseCases.CreateLocation;

public class CreateLocationCommandValidation : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidation()
    {
        RuleFor(x => new{ x.Name })
            .MustBeValueObject(s => LocationName.Create(s.Name));

        RuleFor(x => new{x.City, x.Street, x.HouseNumber, x.Number})
            .MustBeValueObject(s => Address.Create(s.City, s.Street, s.HouseNumber, s.Number));

        RuleFor(x => new{ x.TimeZone })
            .MustBeValueObject(s => TimeZone.Create(s.TimeZone));
    }
}