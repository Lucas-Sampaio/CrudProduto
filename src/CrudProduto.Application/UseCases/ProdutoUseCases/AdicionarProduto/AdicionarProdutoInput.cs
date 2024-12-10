using CrudProduto.Application.Shared;
using CrudProduto.Domain.ProdutoAggregate;
using FluentValidation;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.AdicionarProduto;

public class AdicionarProdutoInput : InputModel<AdicionarProdutoOutput>
{
    public AdicionarProdutoDto Produto { get; set; }

    public override bool EhValido()
    {
        ValidationResult = new AdicionarProdutoValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
public class AdicionarProdutoValidation : AbstractValidator<AdicionarProdutoInput>
{
    public AdicionarProdutoValidation()
    {
        RuleFor(x => x.Produto).NotNull().WithMessage("{PropertyName} nao pode ser nulo");
        When(x => x.Produto is not null, () =>
        {
            RuleFor(x => x.Produto.Codigo)
           .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} tem que ser maior ou igual a {ComparisonValue}");

            RuleFor(x => x.Produto.Nome)
           .MinimumLength(1).WithMessage("O {PropertyName} precisa ter pelo menos {MinLength} caracteres")
           .MaximumLength(Produto.NomeMaximo).WithMessage("O {PropertyName} precisa ter no máximo {MaxLength} caracteres");

            RuleFor(x => x.Produto.Descricao)
                .MaximumLength(Produto.DescricaoMaximo).WithMessage("O {PropertyName} precisa ter no máximo {MaxLength} caracteres");

            RuleFor(x => x.Produto.Valor)
                .GreaterThan(0)
                .WithMessage("O {PropertyName} tem que ser maior que {ComparisonValue}");
        });
       
    }
}