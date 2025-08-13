using DirectoryService.Application.Abstractions;
using DirectoryService.Application.Decorators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
       services.AddValidatorsFromAssembly(assembly);

       services.Scan(scan => scan.FromAssemblies(assembly)
           .AddClasses(classes =>
               classes.AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
           .AsSelfWithInterfaces()
           .WithScopedLifetime());
       
       services.Scan(scan => scan.FromAssemblies(assembly)
           .AddClasses(classes =>
               classes.AssignableToAny(typeof(IQueryHandler<,>)))
           .AsSelfWithInterfaces()
           .WithScopedLifetime());
       
       services.TryDecorate(typeof(ICommandHandler<,>), typeof(CommandDecorator<,>));
       services.TryDecorate(typeof(ICommandHandler<>), typeof(CommandDecorator<>));
       services.TryDecorate(typeof(IQueryHandler<,>), typeof(QueryDecorator<,>));
        
        return services;
    }
    
 
}