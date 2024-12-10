using CrudProduto.Application.UseCases.ProdutoUseCases.AtualizarProduto;
using CrudProduto.Domain.ProdutoAggregate;

namespace CrudProduto.Tests.ApplicationTests.Validators;

public class AtualizarProdutoValidation
{
    [Fact]
    public void EhValido_AtualizarProdutoInputValido_RetornaTrue()
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = 1,
            Produto = new AtualizarProdutoDto
            {
                Descricao = "Teste 1",
                Nome = "Teste 1",
                Valor = 10,
                Tag = "teste"
            }
        };

        var result = input.EhValido();

        Assert.True(result);
    }

    [Fact]
    public void EhValido_AtualizarProdutoInputProdutoNUll_RetornaFalse()
    {
        var input = new AtualizarProdutoInput();

        var result = input.EhValido();

        Assert.False(result);
    }

    [Fact]
    public void ValidationResult_AtualizarProdutoInputNomeMaiorQueOPermitido_RetornaFalse()
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = 1,
            Produto = new AtualizarProdutoDto
            {
                Descricao = "Teste 1",
                Nome = new string('a', Produto.NomeMaximo + 1),
                Valor = 10,
                Tag = "teste"
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Nome precisa ter no máximo {Produto.NomeMaximo} caracteres", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AtualizarProdutoInputNomeNulo_RetornaFalse()
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = 1,
            Produto = new AtualizarProdutoDto
            {
                Descricao = "Teste 1",
                Nome = null,
                Valor = 10,
                Tag = "teste"
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Nome nao pode ser nulo", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AtualizarProdutoInputNomeMenorQueOPermitido_RetornaFalse()
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = 1,
            Produto = new AtualizarProdutoDto
            {
                Descricao = "Teste 1",
                Nome = "",
                Valor = 10,
                Tag = "teste"
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Nome precisa ter pelo menos 1 caracteres", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AtualizarProdutoInputTagMenorQueOPermitido_RetornaFalse()
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = 1,
            Produto = new AtualizarProdutoDto
            {
                Descricao = "Teste 1",
                Nome = "teste",
                Valor = 10,
                Tag = ""
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains($"O Produto Tag precisa ter pelo menos 1 caracteres", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public void ValidationResult_AtualizarProdutoInputTagMaiorQueOPermitido_RetornaFalse()
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = 1,
            Produto = new AtualizarProdutoDto
            {
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
    public void ValidationResult_AtualizarProdutoInputTagNulo_RetornaFalse()
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = 1,
            Produto = new AtualizarProdutoDto
            {
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
    public void ValidationResult_AtualizarProdutoInputDescricaoMaiorQueOPermitido_RetornaFalse()
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = 1,
            Produto = new AtualizarProdutoDto
            {
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
    public void ValidationResult_AtualizarProdutoInputDescricaoValida_Retornatrue(string descricaoValida)
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = 1,
            Produto = new AtualizarProdutoDto
            {
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
    public void ValidationResult_AtualizarProdutoInputValorInvalido_RetornaFalse(decimal valorInvalido)
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = 1,
            Produto = new AtualizarProdutoDto
            {
                Descricao = "Teste 1",
                Nome = "",
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
    public void ValidationResult_AtualizarProdutoInputCodigoNegativo_RetornaErro()
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = -1,
            Produto = new AtualizarProdutoDto
            {
                Descricao = "Teste 1",
                Nome = "Teste 1",
                Valor = 10,
                Tag = "teste"
            }
        };

        var result = input.EhValido();

        Assert.False(result);
        Assert.NotEmpty(input.ValidationResult.Errors);
        Assert.Contains("Codigo tem que ser maior ou igual a 0", input.ValidationResult.Errors.Select(x => x.ErrorMessage));
    }
}