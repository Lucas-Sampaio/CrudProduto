using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CrudProduto.Api.Controllers;

[ApiController]
public abstract class MainController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    protected ICollection<string> Erros = [];

    protected ValidationProblemDetails ObterErroResponse(IEnumerable<ValidationFailure> erros)
    {
        return new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            {"Mensagens",erros.Select(x => x.ErrorMessage).ToArray()},
        });
    }
}