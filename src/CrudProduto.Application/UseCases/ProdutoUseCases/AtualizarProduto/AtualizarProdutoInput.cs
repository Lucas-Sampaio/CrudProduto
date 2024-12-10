using CrudProduto.Application.Shared;
using CrudProduto.Domain.ProdutoAggregate;
using FluentValidation;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.AtualizarProduto;

public class AtualizarProdutoInput : InputModel<AtualizarProdutoOutput>
{
    public int Codigo { get; set; }
    public AtualizarProdutoDto Produto { get; set; }

    public override bool EhValido()
    {
        ValidationResult = new AtualizarProdutoValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class AtualizarProdutoValidation : AbstractValidator<AtualizarProdutoInput>
{
    public AtualizarProdutoValidation()
    {
        RuleFor(x => x.Codigo)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} tem que ser maior ou igual a {ComparisonValue}");

        RuleFor(x => x.Produto).NotNull().WithMessage("{PropertyName} nao pode ser nulo");
        When(x => x.Produto is not null, () =>
        {
            RuleFor(x => x.Produto.Nome)
              .NotNull().WithMessage("O {PropertyName} nao pode ser nulo")
              .MinimumLength(1).WithMessage("O {PropertyName} precisa ter pelo menos {MinLength} caracteres")
              .MaximumLength(Produto.NomeMaximo).WithMessage("O {PropertyName} precisa ter no máximo {MaxLength} caracteres");

            RuleFor(x => x.Produto.Tag)
               .NotNull().WithMessage("O {PropertyName} nao pode ser nulo")
               .MinimumLength(1).WithMessage("O {PropertyName} precisa ter pelo menos {MinLength} caracteres")
               .MaximumLength(Tag.DescricaoMaximo).WithMessage("O {PropertyName} precisa ter no máximo {MaxLength} caracteres");

            RuleFor(x => x.Produto.Descricao)
                .MaximumLength(Produto.DescricaoMaximo).WithMessage("O {PropertyName} precisa ter no máximo {MaxLength} caracteres");

            RuleFor(x => x.Produto.Valor)
                .GreaterThan(0)
                .WithMessage("O {PropertyName} tem que ser maior que {ComparisonValue}");
        });
    }
}

public class AtualizarProdutoDto
{
    /// <summary>
    /// Nome do produto
    /// </summary>

    public string Nome { get; set; }

    /// <summary>
    /// Valor do produto
    /// </summary>

    public decimal Valor { get; set; }

    /// <summary>
    /// descricao do produto
    /// </summary>

    public string Descricao { get; set; }

    /// <summary>
    /// Tag de categoria
    /// </summary>
    public string Tag { get; set; }
}