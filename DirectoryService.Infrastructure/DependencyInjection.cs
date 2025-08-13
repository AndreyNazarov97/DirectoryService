using DirectoryService.Application.UseCases.CreateLocation;
using DirectoryService.Infrastructure.DbContexts;
using DirectoryService.Infrastructure.Options;
using DirectoryService.Infrastructure.Storages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;

namespace DirectoryService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext(configuration)
            .AddStorages();
        
        return services;
    }
    
    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(PostgresOptions.SECTION);
        
        services.AddDbContext<DirectoryServiceDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
    
    private static IServiceCollection AddStorages(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateLocationStorage, CreateLocationStorage>();
        
        
        services.AddScoped<IMomentProvider, MomentProvider>();
        
        return services;
    }
}