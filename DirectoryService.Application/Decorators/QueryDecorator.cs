using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using FluentValidation;
using MyNugets.Errors;

namespace DirectoryService.Application.Decorators;

public class QueryDecorator<TResponse, TQuery> : IQueryHandler<TResponse, TQuery>
    where TQuery : IQuery
{
    private readonly IEnumerable<IValidator<TQuery>> _validators;
    private readonly IQueryHandler<TResponse, TQuery> _inner;

    public QueryDecorator(
        IEnumerable<IValidator<TQuery>> validators,
        IQueryHandler<TResponse, TQuery> inner)
    {
        _validators = validators;
        _inner = inner;
    }

    public async Task<Result<TResponse, ErrorList>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        // Вызываем тот же самый универсальный хелпер, но уже с TQuery
        var errorResult = await ValidationHelper.ValidateAsync(_validators, query, cancellationToken);
        if (errorResult != null)
        {
            return errorResult;
        }

        return await _inner.Handle(query, cancellationToken);
    }
}