using DirectoryService.Application.UseCases.CreateLocation;
using DirectoryService.Contracts.Requests;

namespace DirectoryService.Application.Mappers;

public static class RequestMappers
{
    public static CreateLocationCommand ToCommand(this CreateLocationRequest request)
    {
        return new CreateLocationCommand
        {
            Name = request.Name,
            TimeZone = request.TimeZone,
            City = request.City,
            Street = request.Street,
            HouseNumber = request.HouseNumber,
            Number = request.Number
        };
    }

}