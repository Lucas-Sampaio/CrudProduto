using CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutos;
using CrudProduto.Domain.ProdutoAggregate;
using Moq;

namespace CrudProduto.Tests.ApplicationTests.Usecases;

public class ObterProdutosHandlerTest
{
    private readonly ObterProdutosHandler _handler;
    private readonly Mock<IProdutoRepository> _produtoRepoMock;

    public ObterProdutosHandlerTest()
    {
        _produtoRepoMock = new Mock<IProdutoRepository>();
        _handler = new ObterProdutosHandler(_produtoRepoMock.Object);
    }

    [Fact]
    public async Task Handler_RequestValida_RetornaListaProdutos()
    {
        var codigo1 = 1;
        var nome1 = "teste";
        var valor1 = 10;
        var descricao1 = "";
        var categoria1 = new Tag("categoria 1");
        Produto produto1 = new(codigo1, nome1, valor1, categoria1, descricao1);

        var codigo2 = 2;
        var nome2 = "teste 2";
        var valor2 = 20;
        var descricao2 = "desc 2";
        var categoria2 = new Tag("categoria 2");
        Produto produto2 = new(codigo2, nome2, valor2, categoria2, descricao2);
        var produtos = new List<Produto>(2) { produto1, produto2 };
        _produtoRepoMock
            .Setup(x =>
            x.ObterTodosAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produtos);

        var result = await _handler.Handle(new ObterProdutosInput(), default);

        Assert.NotNull(result);
        Assert.NotNull(result.Produtos);
        Assert.Empty(result.Erros);
        Assert.Collection(result.Produtos, p1 =>
        {
            Assert.Equal(descricao1, p1.Descricao);
            Assert.Equal(nome1, p1.Nome);
            Assert.Equal(valor1, p1.Valor);
            Assert.Equal(codigo1, p1.Codigo);
            Assert.Equal(categoria1.Descricao, p1.Tag);
        },
        p2 =>
        {
            Assert.Equal(descricao2, p2.Descricao);
            Assert.Equal(nome2, p2.Nome);
            Assert.Equal(valor2, p2.Valor);
            Assert.Equal(codigo2, p2.Codigo);
            Assert.Equal(categoria2.Descricao, p2.Tag);
        }
        );

        _produtoRepoMock
           .Verify(x =>
           x.ObterTodosAsync(
               It.IsAny<CancellationToken>()), Times.Once);
    }
}