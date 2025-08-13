using CSharpFunctionalExtensions;
using DirectoryService.Application.Abstractions;
using FluentValidation;
using MyNugets.Errors;

namespace DirectoryService.Application.Decorators;

public class CommandDecorator<TResponse, TCommand> : ICommandHandler<TResponse, TCommand>
    where TCommand : ICommand
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;
    private readonly ICommandHandler<TResponse, TCommand> _inner;

    public CommandDecorator(
        IEnumerable<IValidator<TCommand>> validators,
        ICommandHandler<TResponse, TCommand> inner)
    {
        _validators = validators;
        _inner = inner;
    }

    public async Task<Result<TResponse, ErrorList>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        // Вызываем универсальный хелпер
        var errorResult = await ValidationHelper.ValidateAsync(_validators, command, cancellationToken);
        if (errorResult != null)
        {
            return errorResult;
        }

        return await _inner.Handle(command, cancellationToken);
    }
}

public class CommandDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;
    private readonly ICommandHandler<TCommand> _inner;

    public CommandDecorator(
        IEnumerable<IValidator<TCommand>> validators,
        ICommandHandler<TCommand> inner)
    {
        _validators = validators;
        _inner = inner;
    }

    public async Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken cancellationToken)
    {
        // Вызываем тот же самый универсальный хелпер
        var errorResult = await ValidationHelper.ValidateAsync(_validators, command, cancellationToken);
        if (errorResult != null)
        {
            return errorResult;
        }

        return await _inner.Handle(command, cancellationToken);
    }
}