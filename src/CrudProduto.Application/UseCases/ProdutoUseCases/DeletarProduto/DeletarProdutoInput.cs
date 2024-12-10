using CrudProduto.Application.Shared;
using FluentValidation;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.DeletarProduto;

public class DeletarProdutoInput : InputModel<DeletarProdutoOutput>
{
    public int Codigo { get; set; }

    public override bool EhValido()
    {
        ValidationResult = new DeletarProdutoValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class DeletarProdutoValidation : AbstractValidator<DeletarProdutoInput>
{
    public DeletarProdutoValidation()
    {
        RuleFor(x => x.Codigo)
             .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} tem que ser maior ou igual a {ComparisonValue}");
    }
}