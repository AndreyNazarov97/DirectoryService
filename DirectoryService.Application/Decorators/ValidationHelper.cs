using DirectoryService.Application.Abstractions;
using FluentValidation;
using MyNugets.Errors;

namespace DirectoryService.Application.Decorators;

public static class ValidationHelper
{
    // Метод теперь полностью универсален и работает с любым TRequest,
    // который является ICommand или IQuery.
    public static async Task<ErrorList?> ValidateAsync<TRequest>(
        IEnumerable<IValidator<TRequest>> validators,
        TRequest request,
        CancellationToken cancellationToken) where TRequest : IValidation 
    {
        var validatorList = validators.ToList();
        if (!validatorList.Any())
        {
            return null;
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            validatorList.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(f => f.IsValid == false)
            .ToList();

        if (failures.Any())
        {
            return new ErrorList(
                failures
                    .SelectMany(f => f.Errors)
                    .Select(e => Error.Validation(e.ErrorCode, e.ErrorMessage))
            );
        }

        return null; 
    }
}