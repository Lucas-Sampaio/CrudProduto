using CrudProduto.Application.UseCases.ProdutoUseCases.DeletarProduto;

namespace CrudProduto.Tests.ApplicationTests.Validators;

public class DeletarProdutoValidation
{
    [Fact]
    public void EhValido_DeletarProdutoInputValido_RetornaTrue()
    {
        var input = new DeletarProdutoInput
        {
            Codigo = 1,
        };

        var result = input.EhValido();

        Assert.True(result);
    }

    [Fact]
    public void EhValido_DeletarProdutoInputCodigoNegativo_RetornaFalse()
    {
        var input = new DeletarProdutoInput
        {
            Codigo = -1,
        };

        var result = input.EhValido();

        Assert.False(result);
    }

    [Fact]
    public void ValidationResult_DeletarProdutoInputCodigoNegativo_RetornaErro()
    {
        var input = new DeletarProdutoInput
        {
            Codigo = -1,
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains("Codigo tem que ser maior ou igual a 0", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }
}