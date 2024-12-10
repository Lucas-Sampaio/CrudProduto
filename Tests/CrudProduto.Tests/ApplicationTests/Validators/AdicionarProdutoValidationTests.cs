using CrudProduto.Application.UseCases.ProdutoUseCases.AdicionarProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.AtualizarProduto;
using CrudProduto.Domain.ProdutoAggregate;

namespace CrudProduto.Tests.ApplicationTests.Validators;

public class AdicionarProdutoValidationTests
{
    [Fact]
    public void EhValido_AdicionarProdutoInputValido_RetornaTrue()
    {
        //arange
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = "Teste 1",
                Valor = 10,
                Tag = "teste"
            }
        };

        //act
        var result = input.EhValido();

        //assert
        Assert.True(result);
    }

    [Fact]
    public void EhValido_AdicionarProdutoInputProdutoNUll_RetornaFalse()
    {
        //arange
        var input = new AdicionarProdutoInput();

        //act
        var result = input.EhValido();

        //assert
        Assert.False(result);
    }

    [Fact]
    public void ValidationResult_AdicionarProdutoInputNomeMaiorQueOPermitido_RetornaFalse()
    {
        //arange
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = new string('a', Produto.NomeMaximo + 1),
                Valor = 10,
                Tag = "teste"
            }
        };

        //act
        var result = input.EhValido();

        //assert
        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Nome precisa ter no máximo {Produto.NomeMaximo} caracteres", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AdicionarProdutoInputNomeNulo_RetornaFalse()
    {
        //arange
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = null,
                Valor = 10,
                Tag = "teste"
            }
        };

        //act
        var result = input.EhValido();

        //assert
        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Nome nao pode ser nulo", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AdicionarProdutoInputNomeMenorQueOPermitido_RetornaFalse()
    {
        //arange
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = "",
                Valor = 10,
                Tag = "teste"
            }
        };

        //act
        var result = input.EhValido();

        //assert
        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Nome precisa ter pelo menos 1 caracteres", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AdicionarProdutoInputTagMenorQueOPermitido_RetornaFalse()
    {
        //arange
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = "teste",
                Valor = 10,
                Tag = ""
            }
        };

        //act
        var result = input.EhValido();

        //assert
        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Tag precisa ter pelo menos 1 caracteres", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AdicionarProdutoInputTagMaiorQueOPermitido_RetornaFalse()
    {
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = "Teste 1",
                Valor = 10,
                Tag = new string('a', Tag.DescricaoMaximo + 1)
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Tag precisa ter no máximo {Produto.NomeMaximo} caracteres", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AdicionarProdutoInputTagNulo_RetornaFalse()
    {
        var input = new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = "teste 1",
                Valor = 10,
                Tag = null
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Tag nao pode ser nulo", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
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
                Valor = 10,
                Tag = "teste"
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
                Valor = 10,
                Tag = "teste"
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
                Nome = "teste",
                Valor = valorInvalido,
                Tag = "teste"
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
                Valor = 10,
                Tag = "teste"
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains("Produto Codigo tem que ser maior ou igual a 0", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }
}