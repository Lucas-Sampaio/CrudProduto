﻿using CrudProduto.Application.Shared;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.AdicionarProduto;

public class AdicionarProdutoOutput : OutputModel
{
    public AdicionarProdutoOutput()
    {
    }

    public AdicionarProdutoOutput(int codigo, string nome, decimal valor, string? descricao)
    {
        Produto = new ProdutoResponse
        {
            Codigo = codigo,
            Nome = nome,
            Descricao = descricao,
            Valor = valor,
        };
    }

    public ProdutoResponse Produto { get; set; }
}
