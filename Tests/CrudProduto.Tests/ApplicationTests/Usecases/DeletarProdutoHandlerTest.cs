using CrudProduto.Application.UseCases.ProdutoUseCases.DeletarProduto;
using CrudProduto.Domain.ProdutoAggregate;
using Moq;

namespace CrudProduto.Tests.ApplicationTests.Usecases;

public class DeletarProdutoHandlerTest
{
    private readonly DeletarProdutoHandler _handler;
    private readonly Mock<IProdutoRepository> _produtoRepoMock;

    public DeletarProdutoHandlerTest()
    {
        _produtoRepoMock = new Mock<IProdutoRepository>();
        _handler = new DeletarProdutoHandler(_produtoRepoMock.Object);
    }

    [Fact]
    public async Task Handler_CodigoInValido_RetornaProduto()
    {
        var codigo = -1;
        var nome = "teste";
        var valor = 10;
        var descricao = "";
        var request = new DeletarProdutoInput { Codigo = codigo };
        Produto produto = new(codigo, nome, valor, descricao);

        _produtoRepoMock
            .Setup(x =>
            x.ObterPorCodigoAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produto);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Erros);
        Assert.Contains("Codigo tem que ser maior ou igual a 0", result.Erros.Select(x => x.ErrorMessage));

        _produtoRepoMock
           .Verify(x =>
           x.ObterPorCodigoAsync(
               It.IsAny<int>(),
               It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handler_CodigoValido_DeletaComSucesso()
    {
        var codigo = 1;
        var nome = "teste";
        var valor = 10;
        var descricao = "";
        var request = new DeletarProdutoInput { Codigo = codigo };
        Produto produto = new(codigo, nome, valor, descricao);
        _produtoRepoMock
            .Setup(x =>
            x.ObterPorCodigoAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produto);

        _produtoRepoMock
          .Setup(x =>
          x.UnitOfWork.Commit(It.IsAny<CancellationToken>()))
          .ReturnsAsync(true);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);
        Assert.Empty(result.Erros);
        _produtoRepoMock
           .Verify(x =>
           x.ObterPorCodigoAsync(
               It.Is<int>(x => x == codigo),
               It.IsAny<CancellationToken>()), Times.Once);

        _produtoRepoMock
          .Verify(x =>
          x.Remover(
              It.Is<Produto>(
                  x => x.Codigo == codigo &&
                  x.Nome == nome &&
                  x.Valor == valor &&
                  x.Descricao == descricao
                  )), Times.Once);

        _produtoRepoMock
         .Verify(x =>
         x.UnitOfWork.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handler_ProdutoNulo_ExecutaSemErros()
    {
        var codigo = 1;
        var request = new DeletarProdutoInput { Codigo = codigo };
        Produto produto = null;
        _produtoRepoMock
            .Setup(x =>
            x.ObterPorCodigoAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produto);

        _produtoRepoMock
          .Setup(x =>
          x.UnitOfWork.Commit(It.IsAny<CancellationToken>()))
          .ReturnsAsync(true);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);
        Assert.Empty(result.Erros);
        _produtoRepoMock
           .Verify(x =>
           x.ObterPorCodigoAsync(
               It.Is<int>(x => x == codigo),
               It.IsAny<CancellationToken>()), Times.Once);

        _produtoRepoMock
          .Verify(x =>
          x.Remover(
              It.Is<Produto>(x => x == null)), Times.Once);

        _produtoRepoMock
         .Verify(x =>
         x.UnitOfWork.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }
}