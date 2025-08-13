namespace DirectoryService.Contracts.Requests;

public record CreateLocationRequest(string Name, string TimeZone, string City, string Street, string HouseNumber, string? Number);
