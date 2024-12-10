using CrudProduto.Application.UseCases.ProdutoUseCases.AdicionarProduto;
using CrudProduto.Domain.ProdutoAggregate;
using Moq;

namespace CrudProduto.Tests.ApplicationTests.Usecases;

public class AdicionarProdutoHandlerTest
{
    private readonly AdicionarProdutoHandler _handler;
    private readonly Mock<IProdutoRepository> _produtoRepoMock;

    public AdicionarProdutoHandlerTest()
    {
        _produtoRepoMock = new Mock<IProdutoRepository>();
        _handler = new AdicionarProdutoHandler(_produtoRepoMock.Object);
    }

    [Fact]
    public async Task Handler_ProdutoNovoValido_RetornaProdutoCriado()
    {
        var request = ObterInputValido();
        Produto produto = null;
        var tag = new Tag(request.Produto.Tag);
        _produtoRepoMock
            .Setup(x =>
            x.ObterPorCodigoAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produto);

        _produtoRepoMock
           .Setup(x =>
           x.ObterTagOuAdicionarAsync(
               It.IsAny<string>(),
               It.IsAny<CancellationToken>()))
           .ReturnsAsync(tag);

        _produtoRepoMock
            .Setup(x =>
            x.AdicionarAsync(
                It.IsAny<Produto>(),
                It.IsAny<CancellationToken>()))
            .Returns(ValueTask.CompletedTask);

        _produtoRepoMock
            .Setup(x =>
            x.UnitOfWork.Commit(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        //act
        var result = await _handler.Handle(request, default);

        //assert
        Assert.NotNull(result);
        Assert.NotNull(result.Produto);
        Assert.Empty(result.Erros);
        Assert.Equal(request.Produto.Descricao, result.Produto.Descricao);
        Assert.Equal(request.Produto.Nome, result.Produto.Nome);
        Assert.Equal(request.Produto.Valor, result.Produto.Valor);
        Assert.Equal(request.Produto.Codigo, result.Produto.Codigo);
        Assert.Equal(request.Produto.Tag, result.Produto.Tag);
        _produtoRepoMock
           .Verify(x =>
           x.ObterPorCodigoAsync(
               It.Is<int>(x => x == request.Produto.Codigo),
               It.IsAny<CancellationToken>()), Times.Once);

        _produtoRepoMock
           .Verify(x =>
           x.ObterTagOuAdicionarAsync(
               It.Is<string>(x => x == request.Produto.Tag),
               It.IsAny<CancellationToken>()), Times.Once);

        _produtoRepoMock
            .Verify(x =>
            x.AdicionarAsync(
                It.Is<Produto>(x => x.Codigo == request.Produto.Codigo && x.Nome == request.Produto.Nome),
                It.IsAny<CancellationToken>()), Times.Once);

        _produtoRepoMock
            .Verify(x =>
            x.UnitOfWork.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handler_ProdutoExistente_RetornaErro()
    {
        var request = ObterInputValido();
        Produto produto = new(request.Produto.Codigo, request.Produto.Nome, request.Produto.Valor, new Tag("teste"));
        _produtoRepoMock
            .Setup(x =>
            x.ObterPorCodigoAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produto);

        _produtoRepoMock
          .Setup(x =>
          x.ObterTagOuAdicionarAsync(
              It.IsAny<string>(),
              It.IsAny<CancellationToken>()))
          .ReturnsAsync(new Tag("teste"));

        _produtoRepoMock
            .Setup(x =>
            x.AdicionarAsync(
                It.IsAny<Produto>(),
                It.IsAny<CancellationToken>()))
            .Returns(ValueTask.CompletedTask);

        _produtoRepoMock
            .Setup(x =>
            x.UnitOfWork.Commit(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Erros);
        Assert.Null(result.Produto);

        _produtoRepoMock
           .Verify(x =>
           x.ObterPorCodigoAsync(
               It.Is<int>(x => x == request.Produto.Codigo),
               It.IsAny<CancellationToken>()), Times.Once);

        _produtoRepoMock
            .Verify(x =>
            x.AdicionarAsync(
                It.IsAny<Produto>(),
                It.IsAny<CancellationToken>()), Times.Never);

        _produtoRepoMock
           .Verify(x =>
           x.ObterTagOuAdicionarAsync(
               It.IsAny<string>(),
               It.IsAny<CancellationToken>()), Times.Never);

        _produtoRepoMock
            .Verify(x =>
            x.UnitOfWork.Commit(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handler_RequestInvalida_RetornaErro()
    {
        var request = new AdicionarProdutoInput();
        Produto produto = null;
        _produtoRepoMock
            .Setup(x =>
            x.ObterPorCodigoAsync(
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(produto);

        _produtoRepoMock
            .Setup(x =>
            x.AdicionarAsync(
                It.IsAny<Produto>(),
                It.IsAny<CancellationToken>()))
            .Returns(ValueTask.CompletedTask);

        _produtoRepoMock
         .Setup(x =>
         x.ObterTagOuAdicionarAsync(
             It.IsAny<string>(),
             It.IsAny<CancellationToken>()))
         .ReturnsAsync(new Tag("teste"));

        _produtoRepoMock
            .Setup(x =>
            x.UnitOfWork.Commit(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _handler.Handle(request, default);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Erros);
        Assert.Null(result.Produto);

        _produtoRepoMock
           .Verify(x =>
           x.ObterPorCodigoAsync(
               It.IsAny<int>(),
               It.IsAny<CancellationToken>()), Times.Never);

        _produtoRepoMock
            .Verify(x =>
            x.AdicionarAsync(
                It.IsAny<Produto>(),
                It.IsAny<CancellationToken>()), Times.Never);

        _produtoRepoMock
          .Verify(x =>
          x.ObterTagOuAdicionarAsync(
              It.IsAny<string>(),
              It.IsAny<CancellationToken>()), Times.Never);

        _produtoRepoMock
            .Verify(x =>
            x.UnitOfWork.Commit(It.IsAny<CancellationToken>()), Times.Never);
    }

    private AdicionarProdutoInput ObterInputValido()
    {
        return new AdicionarProdutoInput
        {
            Produto = new AdicionarProdutoDto
            {
                Codigo = 1,
                Descricao = "Teste 1",
                Nome = "Teste 1",
                Valor = 10,
                Tag = "Teste 1"
            }
        };
    }
}