using CrudProduto.Application.UseCases.ProdutoUseCases.ObterProduto;
using CrudProduto.Domain.ProdutoAggregate;
using Moq;

namespace CrudProduto.Tests.ApplicationTests.Usecases;

public class ObterProdutoHandlerTest
{
    private readonly ObterProdutoHandler _handler;
    private readonly Mock<IProdutoRepository> _produtoRepoMock;

    public ObterProdutoHandlerTest()
    {
        _produtoRepoMock = new Mock<IProdutoRepository>();
        _handler = new ObterProdutoHandler(_produtoRepoMock.Object);
    }

    [Fact]
    public async Task Handler_CodigoValido_RetornaProduto()
    {
        var codigo = 1;
        var nome = "teste";
        var valor = 10;
        var descricao = "";
        var request = new ObterProdutoInput { Codigo = codigo };
        Produto produto = new(codigo, nome, valor, descricao);
        _produtoRepoMock
            .Setup(x =>
            x.ObterPorCodigoAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produto);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);
        Assert.NotNull(result.Produto);
        Assert.Empty(result.Erros);
        Assert.Equal(descricao, result.Produto.Descricao);
        Assert.Equal(nome, result.Produto.Nome);
        Assert.Equal(valor, result.Produto.Valor);
        Assert.Equal(codigo, result.Produto.Codigo);

        _produtoRepoMock
           .Verify(x =>
           x.ObterPorCodigoAsync(
               It.Is<int>(x => x == codigo),
               It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handler_CodigoInValido_RetornaErro()
    {
        var codigo = -1;
        var nome = "teste";
        var valor = 10;
        var descricao = "";
        var request = new ObterProdutoInput { Codigo = codigo };
        Produto produto = new(codigo, nome, valor, descricao);
        _produtoRepoMock
            .Setup(x =>
            x.ObterPorCodigoAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produto);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);
        Assert.Null(result.Produto);
        Assert.NotEmpty(result.Erros);
        Assert.Contains("Codigo tem que ser maior ou igual a 0", result.Erros.Select(x => x.ErrorMessage));

        _produtoRepoMock
           .Verify(x =>
           x.ObterPorCodigoAsync(
               It.IsAny<int>(),
               It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handler_ProdutoNaoEncontrado_RetornaErro()
    {
        var codigo = 1;
        var request = new ObterProdutoInput { Codigo = codigo };
        Produto produto = null;
        _produtoRepoMock
            .Setup(x =>
            x.ObterPorCodigoAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produto);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);
        Assert.Null(result.Produto);
        Assert.NotEmpty(result.Erros);
        Assert.Contains("Nao existe um produto com esse codigo", result.Erros.Select(x => x.ErrorMessage));

        _produtoRepoMock
           .Verify(x =>
           x.ObterPorCodigoAsync(
               It.Is<int>(x => x == codigo),
               It.IsAny<CancellationToken>()), Times.Once);
    }
}