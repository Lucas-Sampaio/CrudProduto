using CrudProduto.Domain.ProdutoAggregate;
using CrudProduto.Infra;
using CrudProduto.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrudProduto.Tests.Infra.Fixtures;

[CollectionDefinition(nameof(ProdutoRepositoryCollection))]
public class ProdutoRepositoryCollection : ICollectionFixture<ProdutoRepositoryFixture>
{
}

/// <summary>
/// Cria um repositorio de produto salvando na memoria
/// </summary>
public class ProdutoRepositoryFixture
{
    public ProdutoRepository ObterProdutoRepository()
    {
        var options = new DbContextOptionsBuilder<CrudProdutoContext>()
            .UseInMemoryDatabase(databaseName: "CrudProduto").Options;
        var context = new CrudProdutoContext(options);
        return new ProdutoRepository(context);
    }

    public Produto GerarProdutoValido(int codigo, string nome = "teste 1", decimal valor = 10, string descricao = "", string tag = "categoria teste")
    {
        return new Produto(codigo, nome, valor, new Tag(tag), descricao);
    }
}