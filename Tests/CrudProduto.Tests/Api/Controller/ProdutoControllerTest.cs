using CrudProduto.Api.Controllers;
using CrudProduto.Application.Shared;
using CrudProduto.Application.UseCases.ProdutoUseCases.AdicionarProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.AtualizarProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.DeletarProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.ObterProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CrudProduto.Tests.Api.Controller;

public class ProdutoControllerTest
{
    private ProdutoController _produtoController;
    private Mock<IMediator> _mediatorMock;

    public ProdutoControllerTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _produtoController = new ProdutoController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Post_RequestValida_RetornaCreated()
    {
        //arrange
        var codigo = 1;
        var nome = "teste 1";
        var valor = 10;
        var request = new AdicionarProdutoDto
        {
            Codigo = codigo,
            Descricao = "",
            Nome = nome,
            Valor = valor
        };
        var response = new AdicionarProdutoOutput()
        {
            Produto = new ProdutoResponse
            {
                Codigo = codigo,
                Valor = valor,
                Nome = nome
            }
        };
        _mediatorMock.Setup(x => x.Send(
            It.IsAny<IRequest<AdicionarProdutoOutput>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        //act
        var result = await _produtoController.Post(request, default);

        //assert
        Assert.IsType<CreatedResult>(result);
        _mediatorMock.Verify(x => x.Send(
           It.IsAny<IRequest<AdicionarProdutoOutput>>(),
           It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(((CreatedResult)result).Value);
    }

    [Fact]
    public async Task Post_RequestComErro_RetornaBadRequest()
    {
        //arrange
        var codigo = 1;
        var nome = "teste 1";
        var valor = 10;
        var request = new AdicionarProdutoDto
        {
            Codigo = codigo,
            Descricao = "",
            Nome = nome,
            Valor = valor
        };
        var response = new AdicionarProdutoOutput();
        response.AdicionarErro("Erro teste");

        _mediatorMock.Setup(x => x.Send(
            It.IsAny<IRequest<AdicionarProdutoOutput>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        //act
        var result = await _produtoController.Post(request, default);

        //assert
        Assert.IsType<BadRequestObjectResult>(result);
        _mediatorMock.Verify(x => x.Send(
           It.IsAny<IRequest<AdicionarProdutoOutput>>(),
           It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Get_RequestValida_RetornaOk()
    {
        //arrange
        var codigo = 1;

        var response = new ObterProdutoOutput()
        {
            Produto = new ProdutoResponse
            {
                Codigo = codigo,
                Valor = 10,
                Nome = "teste"
            }
        };
        _mediatorMock.Setup(x => x.Send(
            It.IsAny<IRequest<ObterProdutoOutput>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        //act
        var result = await _produtoController.Get(codigo, default);

        //assert
        Assert.IsType<OkObjectResult>(result);
        _mediatorMock.Verify(x => x.Send(
           It.IsAny<IRequest<ObterProdutoOutput>>(),
           It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(((OkObjectResult)result).Value);
    }

    [Fact]
    public async Task Get_RequestComErro_RetornaBadRequest()
    {
        //arrange
        var codigo = 1;
        var response = new ObterProdutoOutput();
        response.AdicionarErro("Erro teste");

        _mediatorMock.Setup(x => x.Send(
            It.IsAny<IRequest<ObterProdutoOutput>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        //act
        var result = await _produtoController.Get(codigo, default);

        //assert
        Assert.IsType<BadRequestObjectResult>(result);
        _mediatorMock.Verify(x => x.Send(
           It.IsAny<IRequest<ObterProdutoOutput>>(),
           It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Get_ObtemTodosProdutos_RetornaOk()
    {
        //arrange

        var response = new ObterProdutosOutput()
        {
            Produtos =
            [
                new()
                {
                    Codigo = 1,
                    Valor = 10,
                    Nome = "teste"
                }
            ]
        };
        _mediatorMock.Setup(x => x.Send(
            It.IsAny<IRequest<ObterProdutosOutput>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        //act
        var result = await _produtoController.Get(default);

        //assert
        Assert.IsType<OkObjectResult>(result);
        _mediatorMock.Verify(x => x.Send(
           It.IsAny<IRequest<ObterProdutosOutput>>(),
           It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(((OkObjectResult)result).Value);
    }

    [Fact]
    public async Task Delete_RequestValida_RetornaNoContent()
    {
        //arrange
        var codigo = 1;

        var response = new DeletarProdutoOutput();

        _mediatorMock.Setup(x => x.Send(
            It.IsAny<IRequest<DeletarProdutoOutput>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        //act
        var result = await _produtoController.Delete(codigo, default);

        //assert
        Assert.IsType<NoContentResult>(result);
        _mediatorMock.Verify(x => x.Send(
           It.IsAny<IRequest<DeletarProdutoOutput>>(),
           It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_RequestComErro_RetornaBadRequest()
    {
        //arrange
        var codigo = 1;
        var response = new DeletarProdutoOutput();
        response.AdicionarErro("Erro teste");

        _mediatorMock.Setup(x => x.Send(
            It.IsAny<IRequest<DeletarProdutoOutput>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        //act
        var result = await _produtoController.Delete(codigo, default);

        //assert
        Assert.IsType<BadRequestObjectResult>(result);
        _mediatorMock.Verify(x => x.Send(
           It.IsAny<IRequest<DeletarProdutoOutput>>(),
           It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Put_RequestValida_RetornaNoContent()
    {
        //arrange
        var codigo = 1;

        var response = new AtualizarProdutoOutput();

        _mediatorMock.Setup(x => x.Send(
            It.IsAny<IRequest<AtualizarProdutoOutput>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        //act
        var result = await _produtoController.Put(codigo, new AtualizarProdutoDto(), default);

        //assert
        Assert.IsType<NoContentResult>(result);
        _mediatorMock.Verify(x => x.Send(
           It.IsAny<IRequest<AtualizarProdutoOutput>>(),
           It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Put_RequestComErro_RetornaBadRequest()
    {
        //arrange
        var codigo = 1;
        var response = new AtualizarProdutoOutput();
        response.AdicionarErro("Erro teste");

        _mediatorMock.Setup(x => x.Send(
            It.IsAny<IRequest<AtualizarProdutoOutput>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        //act
        var result = await _produtoController.Put(codigo, new AtualizarProdutoDto(), default);

        //assert
        Assert.IsType<BadRequestObjectResult>(result);
        _mediatorMock.Verify(x => x.Send(
           It.IsAny<IRequest<AtualizarProdutoOutput>>(),
           It.IsAny<CancellationToken>()), Times.Once);
    }
}