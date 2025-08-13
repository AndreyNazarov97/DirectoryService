using DirectoryService.Application.Abstractions;

namespace DirectoryService.Application.UseCases.CreateLocation;

public record CreateLocationCommand : ICommand
{
   public required string Name { get; init; }
   public required string TimeZone { get; init; }
   public required string City { get; init; }
   public required string Street { get; init; }
   public required string HouseNumber { get; init; }
   public string? Number { get; init; }
}