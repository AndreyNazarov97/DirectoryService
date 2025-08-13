using CSharpFunctionalExtensions;
using DirectoryService.Application.UseCases.CreateLocation;
using DirectoryService.Domain.Entities;
using DirectoryService.Infrastructure.DbContexts;
using Microsoft.Extensions.Logging;
using MyNugets.Errors;

namespace DirectoryService.Infrastructure.Storages;

public class CreateLocationStorage : ICreateLocationStorage
{
    private readonly DirectoryServiceDbContext _context;
    private readonly IMomentProvider _momentProvider;
    private readonly ILogger<CLSCompliantAttribute> _logger;

    public CreateLocationStorage(
        DirectoryServiceDbContext context,
        IMomentProvider momentProvider,
        ILogger<CLSCompliantAttribute> logger)
    {
        _context = context;
        _momentProvider = momentProvider;
        _logger = logger;
    }
    
    public async Task<Result<Location, ErrorList>> Create(Location location)
    {
        try
        {
            location.SetCreatedAt(_momentProvider.Now);
            location.SetUpdatedAt(_momentProvider.Now);
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync(CancellationToken.None);
        
            return location;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Error.Failure("location.create", e.Message).ToErrorList();
        }
    }
}