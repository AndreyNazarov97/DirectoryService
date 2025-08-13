using CSharpFunctionalExtensions;
using MyNugets.Errors;

namespace DirectoryService.Application.Abstractions;

public interface IQuery : IValidation;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    Task<Result<TResponse, ErrorList>> Handle(TQuery query, CancellationToken cancellationToken = default);
}