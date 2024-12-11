using CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutoPorTag;
using CrudProduto.Domain.ProdutoAggregate;
using Moq;

namespace CrudProduto.Tests.ApplicationTests.Usecases;

public class ObterProdutosPorTagHandlerTest
{
    private readonly ObterProdutosPorTagHandler _handler;
    private readonly Mock<IProdutoRepository> _produtoRepoMock;

    public ObterProdutosPorTagHandlerTest()
    {
        _produtoRepoMock = new Mock<IProdutoRepository>();
        _handler = new ObterProdutosPorTagHandler(_produtoRepoMock.Object);
    }

    [Fact]
    public async Task Handler_TagValida_RetornaProdutos()
    {
        var tag = "tag 1";
        var nome = "teste";
        var valor = 10;
        var descricao = "";
        var codigo = 1;
        var request = new ObterProdutosPorTagInput { Tag = tag };
        Produto produto = new(codigo, nome, valor, new Tag(tag), descricao);
        var produtos = new List<Produto>(1)
        {
            produto
        };
        _produtoRepoMock
            .Setup(x =>
            x.ObterPorTagAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produtos);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);
        Assert.NotNull(result.Produtos);
        Assert.Empty(result.Erros);
        Assert.Equal(descricao, result.Produtos[0].Descricao);
        Assert.Equal(nome, result.Produtos[0].Nome);
        Assert.Equal(valor, result.Produtos[0].Valor);
        Assert.Equal(codigo, result.Produtos[0].Codigo);
        Assert.Equal(produto.Tag.Descricao, result.Produtos[0].Tag);

        _produtoRepoMock
           .Verify(x =>
           x.ObterPorTagAsync(
               It.Is<string>(x => x == tag),
               It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handler_CodigoInValido_RetornaErro()
    {
        var tag = "";
        var nome = "teste";
        var valor = 10;
        var descricao = "";
        var codigo = 1;
        var request = new ObterProdutosPorTagInput { Tag = tag };
        Produto produto = new(codigo, nome, valor, new Tag(tag), descricao);
        var produtos = new List<Produto>(1)
        {
            produto
        };
        _produtoRepoMock
            .Setup(x =>
            x.ObterPorTagAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produtos);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);
        Assert.Null(result.Produtos);
        Assert.NotEmpty(result.Erros);
        Assert.Contains("Tag nao pode ser vazio", result.Erros.Select(x => x.ErrorMessage));

        _produtoRepoMock
           .Verify(x =>
           x.ObterPorTagAsync(
               It.IsAny<string>(),
               It.IsAny<CancellationToken>()), Times.Never);
    }
}