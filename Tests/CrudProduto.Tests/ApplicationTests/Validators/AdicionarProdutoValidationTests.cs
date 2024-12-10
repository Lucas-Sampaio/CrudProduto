using CrudProduto.Application.UseCases.ProdutoUseCases.AdicionarProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.AtualizarProduto;
using CrudProduto.Domain.ProdutoAggregate;

namespace CrudProduto.Tests.ApplicationTests.Validators;

public class AdicionarProdutoValidationTests
{
    [Fact]
    public void EhValido_AdicionarProdutoInputValido_RetornaTrue()
    {
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = "Teste 1",
                Valor = 10
            }
        };

        var result = input.EhValido();

        Assert.True(result);
    }

    [Fact]
    public void EhValido_AdicionarProdutoInputProdutoNUll_RetornaFalse()
    {
        var input = new AdicionarProdutoInput();

        var result = input.EhValido();

        Assert.False(result);
    }

    [Fact]
    public void ValidationResult_AdicionarProdutoInputNomeMaiorQueOPermitido_RetornaFalse()
    {
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = new string('a', Produto.NomeMaximo + 1),
                Valor = 10
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Nome precisa ter no máximo {Produto.NomeMaximo} caracteres", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AdicionarProdutoInputNomeMenorQueOPermitido_RetornaFalse()
    {
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = "",
                Valor = 10
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Nome precisa ter pelo menos 1 caracteres", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AdicionarProdutoInputDescricaoMaiorQueOPermitido_RetornaFalse()
    {
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = new string('a', Produto.DescricaoMaximo + 1),
                Nome = "teste",
                Valor = 10
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Descricao precisa ter no máximo {Produto.DescricaoMaximo} caracteres", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("teste 1")]
    public void ValidationResult_AdicionarProdutoInputDescricaoValida_Retornatrue(string descricaoValida)
    {
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = descricaoValida,
                Nome = "teste",
                Valor = 10
            }
        };

        var result = input.EhValido();

        Assert.True(result);
        Assert.Empty(input.ValidationResult.Errors);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void ValidationResult_AdicionarProdutoInputValorInvalido_RetornaFalse(decimal valorInvalido)
    {
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = "",
                Valor = valorInvalido
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Valor tem que ser maior que 0", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AdicionarProdutoInputCodigoNegativo_RetornaErro()
    {
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = -1,
                Descricao = "Teste 1",
                Nome = "Teste 1",
                Valor = 10
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains("Produto Codigo tem que ser maior ou igual a 0", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }
}