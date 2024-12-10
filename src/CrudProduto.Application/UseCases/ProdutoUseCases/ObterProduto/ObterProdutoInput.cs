using CrudProduto.Application.Shared;
using FluentValidation;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.ObterProduto;

public class ObterProdutoInput : InputModel<ObterProdutoOutput>
{
    public int Codigo { get; set; }

    public override bool EhValido()
    {
        ValidationResult = new ObterProdutoValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class ObterProdutoValidation : AbstractValidator<ObterProdutoInput>
{
    public ObterProdutoValidation()
    {
        RuleFor(x => x.Codigo)
             .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} tem que ser maior ou igual a {ComparisonValue}");
    }
}