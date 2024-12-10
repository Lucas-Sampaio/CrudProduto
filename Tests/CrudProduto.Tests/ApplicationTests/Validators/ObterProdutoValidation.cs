using CrudProduto.Application.UseCases.ProdutoUseCases.ObterProduto;

namespace CrudProduto.Tests.ApplicationTests.Validators;

public class ObterProdutoValidation
{
    [Fact]
    public void EhValido_ObterProdutoInputValido_RetornaTrue()
    {
        var input = new ObterProdutoInput
        {
            Codigo = 1,
        };

        var result = input.EhValido();

        Assert.True(result);
    }

    [Fact]
    public void EhValido_ObterProdutoInputCodigoNegativo_RetornaFalse()
    {
        var input = new ObterProdutoInput
        {
            Codigo = -1,
        };

        var result = input.EhValido();

        Assert.False(result);
    }

    [Fact]
    public void ValidationResult_ObterProdutoInputCodigoNegativo_RetornaErro()
    {
        var input = new ObterProdutoInput
        {
            Codigo = -1,
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains("Codigo tem que ser maior ou igual a 0", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }
}