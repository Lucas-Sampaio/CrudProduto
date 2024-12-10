using FluentValidation.Results;
using MediatR;

namespace CrudProduto.Application.Shared;

public abstract class InputModel<TOutPutModel> : IRequest<TOutPutModel> where TOutPutModel : OutputModel
{
    public ValidationResult? ValidationResult { get; protected set; }

    public abstract bool EhValido();
}