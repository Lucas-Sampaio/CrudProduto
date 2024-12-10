using CrudProduto.Application.Shared;
using CrudProduto.Application.UseCases.ProdutoUseCases.AdicionarProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.AtualizarProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.DeletarProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.ObterProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CrudProduto.Api.Controllers;

[Route("api/[controller]")]
public class ProdutoController(IMediator mediator) : MainController(mediator)
{
    /// <summary>
    /// Cadastra um novo produto
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns>retorna o produto que foi criado</returns>
    [HttpPost("")]
    [ProducesResponseType(typeof(ProdutoResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Post(AdicionarProdutoDto request, CancellationToken ct)
    {
        var input = new AdicionarProdutoInput
        {
            Produto = request
        };

        var response = await _mediator.Send(input, ct);
        if (response.Erros.Count > 0)
            return BadRequest(ObterErroResponse(response.Erros));

        return Created("", response.Produto);
    }

    /// <summary>
    /// Obtem todos os produtos
    /// </summary>
    /// <param name="ct"></param>
    /// <returns>retorna uma lista de produtos</returns>
    [HttpGet("")]
    [ProducesResponseType(typeof(ProdutoResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var input = new ObterProdutosInput();

        var response = await _mediator.Send(input, ct);

        return Ok(response.Produtos);
    }
     
    /// <summary>
    /// Obtem um produto por codigo
    /// </summary>
    /// <param name="codigo">codigo do produto</param>
    /// <param name="ct"></param>
    /// <returns>retorna um produto</returns>
    [HttpGet("{codigo}")]
    [ProducesResponseType(typeof(ProdutoResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Get(int codigo, CancellationToken ct)
    {
        var input = new ObterProdutoInput
        {
            Codigo = codigo,
        };

        var response = await _mediator.Send(input, ct);
        if (response.Erros.Count > 0)
            return BadRequest(ObterErroResponse(response.Erros));

        return Ok(response.Produto);
    }

    /// <summary>
    /// Obtem um produto por codigo
    /// </summary>
    /// <param name="codigo">codigo do produto</param>
    /// <param name="ct"></param>
    /// <returns>retorna um produto</returns>
    [HttpDelete("{codigo}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Delete(int codigo, CancellationToken ct)
    {
        var input = new DeletarProdutoInput
        {
            Codigo = codigo,
        };

        var response = await _mediator.Send(input, ct);
        if (response.Erros.Count > 0)
            return BadRequest(ObterErroResponse(response.Erros));

        return NoContent();
    }
    /// <summary>
    /// Atualiza informacoes de um produto
    /// </summary>
    /// <param name="codigo">Codigo do produto a ser atualizado</param>
    /// <param name="request">propriedades a serem atualizadas</param>
    /// <param name="ct"></param>
    /// <returns>retorna o produto que foi criado</returns>
    [HttpPut("{codigo}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Put(int codigo, AtualizarProdutoDto request, CancellationToken ct)
    {
        var input = new AtualizarProdutoInput
        {
            Codigo = codigo,
            Produto = request
        };

        var response = await _mediator.Send(input, ct);
        if (response.Erros.Count > 0)
            return BadRequest(ObterErroResponse(response.Erros));

        return NoContent();
    }
}