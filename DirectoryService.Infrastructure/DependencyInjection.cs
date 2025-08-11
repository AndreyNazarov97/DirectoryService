using DirectoryService.Infrastructure.DbContexts;
using DirectoryService.Infrastructure.Options;
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
            .AddRepositories();
        
        return services;
    }
    
    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(PostgresOptions.SECTION);
        
        services.AddDbContext<DirectoryServiceDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        
        return services;
    }
}