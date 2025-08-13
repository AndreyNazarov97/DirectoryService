using CSharpFunctionalExtensions;
using DirectoryService.Domain.Entities;
using MyNugets.Errors;

namespace DirectoryService.Application.UseCases.CreateLocation;

public interface ICreateLocationStorage
{
    public Task<Result<Location,ErrorList>> Create(Location location);
}