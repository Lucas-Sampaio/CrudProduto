using CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutoPorTag;

namespace CrudProduto.Tests.ApplicationTests.Validators;

public class ObterProdutoPorTagValidationTests
{
    [Fact]
    public void EhValido_ObterProdutosPorTagInputValido_RetornaTrue()
    {
        var input = new ObterProdutosPorTagInput
        {
            Tag = "tag",
        };

        var result = input.EhValido();

        Assert.True(result);
        Assert.Empty(input.ValidationResult.Errors);
    }

    [Fact]
    public void EhValido_ObterProdutosPorTagInputTagNull_RetornaFalse()
    {
        var input = new ObterProdutosPorTagInput
        {
            Tag = null,
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.Contains($"Tag nao pode ser nulo", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void EhValido_ObterProdutosPorTagInputTagVazia_RetornaFalse()
    {
        var input = new ObterProdutosPorTagInput
        {
            Tag = "",
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.Contains($"Tag nao pode ser vazio", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }
}