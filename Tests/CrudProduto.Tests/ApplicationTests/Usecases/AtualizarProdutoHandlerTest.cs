using CrudProduto.Application.UseCases.ProdutoUseCases.AtualizarProduto;
using CrudProduto.Domain.ProdutoAggregate;
using Moq;

namespace CrudProduto.Tests.ApplicationTests.Usecases;

public class AtualizarProdutoHandlerTest
{
    private readonly AtualizarProdutoHandler _handler;
    private readonly Mock<IProdutoRepository> _produtoRepoMock;

    public AtualizarProdutoHandlerTest()
    {
        _produtoRepoMock = new Mock<IProdutoRepository>();
        _handler = new AtualizarProdutoHandler(_produtoRepoMock.Object);
    }

    [Fact]
    public async Task Handler_ProdutoValido_AtualizaComSucesso()
    {
        //arrange
        var request = ObterInputValido();
        Produto produto = new(request.Codigo, "produto base", 10, new Tag("teste"));
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

        _produtoRepoMock
           .Setup(x =>
           x.ObterTagOuAdicionarAsync(
               It.IsAny<string>(),
               It.IsAny<CancellationToken>()))
           .ReturnsAsync(produto.Tag);

        //act
        var result = await _handler.Handle(request, default);

        //assert
        Assert.NotNull(result);
        Assert.Empty(result.Erros);

        _produtoRepoMock
           .Verify(x =>
           x.ObterPorCodigoAsync(
               It.Is<int>(x => x == request.Codigo),
               It.IsAny<CancellationToken>()), Times.Once);

        _produtoRepoMock
          .Verify(x =>
          x.Atualizar(
              It.Is<Produto>(
                  x => x.Codigo == produto.Codigo &&
                  x.Nome == request.Produto.Nome &&
                  x.Valor == request.Produto.Valor &&
                  x.Descricao == request.Produto.Descricao
                  )), Times.Once);

        _produtoRepoMock
           .Verify(x =>
           x.ObterTagOuAdicionarAsync(
               It.Is<string>(x => x == request.Produto.Tag),
               It.IsAny<CancellationToken>()), Times.Once);
        _produtoRepoMock
            .Verify(x =>
            x.UnitOfWork.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handler_ProdutoNaoEncontrado_RetornaErro()
    {
        //arrange
        var request = ObterInputValido();
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

        _produtoRepoMock
           .Setup(x =>
           x.ObterTagOuAdicionarAsync(
               It.IsAny<string>(),
               It.IsAny<CancellationToken>()))
           .ReturnsAsync(new Tag("teste"));

        //act
        var result = await _handler.Handle(request, default);

        //assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Erros);
        Assert.Contains("Nao existe um produto com esse codigo", result.Erros.Select(x => x.ErrorMessage));

        _produtoRepoMock
         .Verify(x =>
         x.ObterPorCodigoAsync(
             It.Is<int>(x => x == request.Codigo),
             It.IsAny<CancellationToken>()), Times.Once);

        _produtoRepoMock
         .Verify(x =>
         x.Atualizar(
             It.IsAny<Produto>()), Times.Never);

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
    public async Task Handler_RequestFalhaValidacao_RetornaErro()
    {
        //arrange
        var request = new AtualizarProdutoInput();
        Produto produto = new(1, "produto novo", 10, new Tag("teste")); ;
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

        _produtoRepoMock
           .Setup(x =>
           x.ObterTagOuAdicionarAsync(
               It.IsAny<string>(),
               It.IsAny<CancellationToken>()))
           .ReturnsAsync(new Tag("teste"));

        //act
        var result = await _handler.Handle(request, default);

        //asset
        Assert.NotNull(result);
        Assert.NotEmpty(result.Erros);

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

    private AtualizarProdutoInput ObterInputValido(int codigo = 1)
    {
        return new AtualizarProdutoInput
        {
            Codigo = codigo,
            Produto = new AtualizarProdutoDto
            {
                Descricao = "Teste 1",
                Nome = "Teste 1",
                Valor = 10,
                Tag = "teste"
            }
        };
    }
}