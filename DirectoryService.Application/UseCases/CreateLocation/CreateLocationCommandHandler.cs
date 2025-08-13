using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using DirectoryService.Domain.Entities;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.EntityIds;
using Microsoft.Extensions.Logging;
using MyNugets.Errors;
using TimeZone = DirectoryService.Domain.ValueObjects.TimeZone;

namespace DirectoryService.Application.UseCases.CreateLocation;

public class CreateLocationCommandHandler : ICommandHandler<Location, CreateLocationCommand>
{
    private readonly ICreateLocationStorage _storage;
    private readonly ILogger<CreateLocationCommandHandler> _logger;

    public CreateLocationCommandHandler(
        ICreateLocationStorage storage,
        ILogger<CreateLocationCommandHandler> logger)
    {
        _storage = storage;
        _logger = logger;
    }
    
    public async Task<Result<Location, ErrorList>> Handle(CreateLocationCommand command, CancellationToken cancellationToken)
    {
        var timeZoneResult = TimeZone.Create(command.TimeZone);
        if (timeZoneResult.IsFailure)
        {
            _logger.LogError(timeZoneResult.Error.Serialize());
            return timeZoneResult.Error.ToErrorList();
        }
        
        var locationNameResult = LocationName.Create(command.Name);
        if (locationNameResult.IsFailure)
        {
            _logger.LogError(locationNameResult.Error.Serialize());
            return locationNameResult.Error.ToErrorList();
        }
        
        var addressResult = Address.Create(command.City, command.Street, command.HouseNumber, command.Number);
        if (addressResult.IsFailure)
        {
            _logger.LogError(addressResult.Error.Serialize());
            return addressResult.Error.ToErrorList();
        }

        var location = new Location(LocationId.NewId(), locationNameResult.Value, timeZoneResult.Value);
        location.AddAddress(addressResult.Value);
    
        var result = await _storage.Create(location);
        
        if(result.IsFailure)
            return result.Error;
        
        _logger.LogInformation("Location created with id: {Id}, name: {Name}", result.Value.Id, result.Value.Name.Value);
        return result;
    }
}