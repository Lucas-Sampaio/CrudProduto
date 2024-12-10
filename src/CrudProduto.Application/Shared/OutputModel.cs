using FluentValidation.Results;

namespace CrudProduto.Application.Shared;

public abstract class OutputModel
{
    protected OutputModel()
    {
        Erros = [];
    }

    public List<ValidationFailure> Erros { get; }

    public void AdicionarErros(IEnumerable<ValidationFailure> erros) =>
        Erros.AddRange(erros);

    public void AdicionarErro(string erro) =>
        Erros.Add(new() { ErrorMessage = erro });
}