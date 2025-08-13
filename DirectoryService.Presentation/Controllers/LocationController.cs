using DirectoryService.Application.Mappers;
using DirectoryService.Application.UseCases.CreateLocation;
using DirectoryService.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using MyNugets.Errors.Extensions;

namespace DirectoryService.Presentation.Controllers;
[Route("api")]
public class LocationController : ApplicationController  
{
    [HttpPost()]
    public async Task<IActionResult> CreateLocation(
        [FromServices] CreateLocationCommandHandler handler,
        [FromBody] CreateLocationRequest request,
        CancellationToken cancellationToken)
    {
       var command = request.ToCommand();
       
       var result = await handler.Handle(command, cancellationToken);
       
       if (result.IsFailure)
           return result.Error.ToResponse();

       return Ok(result.Value);
    }
}