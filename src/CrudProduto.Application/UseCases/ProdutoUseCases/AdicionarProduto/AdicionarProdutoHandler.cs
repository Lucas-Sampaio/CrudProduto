﻿using CrudProduto.Application.Shared;
using CrudProduto.Domain.ProdutoAggregate;
using MediatR;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.AdicionarProduto;

public class AdicionarProdutoHandler(IProdutoRepository produtoRepository) : IRequestHandler<AdicionarProdutoInput, AdicionarProdutoOutput>
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));

    public async Task<AdicionarProdutoOutput> Handle(AdicionarProdutoInput request, CancellationToken cancellationToken)
    {
        var outputModel = new AdicionarProdutoOutput();
        var produtoDto = request.Produto;
        if (!request.EhValido())
        {
            outputModel.AdicionarErros(request.ValidationResult.Errors);
            return outputModel;
        }

        var produto = await _produtoRepository.ObterPorCodigoAsync(produtoDto.Codigo, cancellationToken);
        if (produto is not null)
        {
            outputModel.AdicionarErro("Ja existe um produto com esse codigo");
            return outputModel;
        }

        var tag = await _produtoRepository.ObterTagOuAdicionarAsync(request.Produto.Tag, cancellationToken);

        produto = new Produto(produtoDto.Codigo, produtoDto.Nome, produtoDto.Valor, tag, produtoDto.Descricao);
        await _produtoRepository.AdicionarAsync(produto, cancellationToken);
        await _produtoRepository.UnitOfWork.Commit(cancellationToken);

        outputModel.Produto = new ProdutoResponse
        {
            Codigo = produto.Codigo,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Valor = produto.Valor,
            Tag = produto.Tag.Descricao
        };
        return outputModel;
    }
}