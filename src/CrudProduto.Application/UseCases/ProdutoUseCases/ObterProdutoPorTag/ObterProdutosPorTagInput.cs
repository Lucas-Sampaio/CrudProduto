using CrudProduto.Application.Shared;
using FluentValidation;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutoPorTag;

public class ObterProdutosPorTagInput : InputModel<ObterProdutosPorTagOutput>
{
    public string Tag { get; set; }

    public override bool EhValido()
    {
        ValidationResult = new ObterProdutoPorTagValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class ObterProdutoPorTagValidation : AbstractValidator<ObterProdutosPorTagInput>
{
    public ObterProdutoPorTagValidation()
    {
        RuleFor(x => x.Tag)
            .NotNull().WithMessage("{PropertyName} nao pode ser nulo")
            .NotEmpty().WithMessage("{PropertyName} nao pode ser vazio");
    }
}