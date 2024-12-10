using CrudProduto.Tests.Infra.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace CrudProduto.Tests.Infra.Repositories;

[Collection(nameof(ProdutoRepositoryCollection))]
public class ProdutoRepositoryTest(ProdutoRepositoryFixture produtoRepositoryFixture)
{
    private readonly ProdutoRepositoryFixture _produtoRepositoryFixture = produtoRepositoryFixture;

    [Fact]
    public async Task Adicionar_NovoProdutoValido_DeveExecutarComSucesso()
    {
        // Arrange
        var codigo = 1;
        var repositorio = _produtoRepositoryFixture.ObterProdutoRepository();
        var produto = _produtoRepositoryFixture.GerarProdutoValido(codigo);

        //act
        await repositorio.AdicionarAsync(produto, default);
        await repositorio.UnitOfWork.Commit(default);
        var result = await repositorio.ObterPorCodigoAsync(codigo, default);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(codigo, result.Codigo);
    }

    [Fact]
    public async Task Adicionar_ProdutoExistente_RetornarException()
    {
        // Arrange
        var codigo = 2;
        var repositorio = _produtoRepositoryFixture.ObterProdutoRepository();
        var produto = _produtoRepositoryFixture.GerarProdutoValido(codigo);
        await repositorio.AdicionarAsync(produto, default);
        await repositorio.UnitOfWork.Commit(default);

        //act
        await repositorio.AdicionarAsync(produto, default);
        var excecao = await Assert.ThrowsAsync<ArgumentException>(async () => await repositorio.UnitOfWork.Commit(default));

        //Assert
        Assert.NotNull(excecao);
        Assert.NotEmpty(excecao.Message);
        Assert.Contains("with the same key", excecao.Message);
    }

    [Fact]
    public async Task Adicionar_ProdutoNomeNulo_RetornarException()
    {
        // Arrange
        var codigo = 3;
        var repositorio = _produtoRepositoryFixture.ObterProdutoRepository();
        // var nome = null;
        var produto = _produtoRepositoryFixture.GerarProdutoValido(codigo, null);

        //act
        await repositorio.AdicionarAsync(produto, default);

        var excecao = await Assert.ThrowsAsync<DbUpdateException>(async () => await repositorio.UnitOfWork.Commit(default));

        //Assert
        Assert.NotNull(excecao);
        Assert.NotEmpty(excecao.Message);
        Assert.Contains("Required properties '{'Nome'}'", excecao.Message);
    }

    [Fact]
    public async Task Atualizar_ProdutoValido_AtualizaComSucesso()
    {
        // Arrange
        var codigo = 4;
        var repositorio = _produtoRepositoryFixture.ObterProdutoRepository();
        var produto = _produtoRepositoryFixture.GerarProdutoValido(codigo);
        await repositorio.AdicionarAsync(produto, default);
        await repositorio.UnitOfWork.Commit(default);
        var produtoBase = await repositorio.ObterPorCodigoAsync(codigo, default);
        var nomeAtt = "Teste atualizacao";
        var valorAtt = 20;
        var descricaoAtt = "Descricao atualizado";
        produtoBase.Alterar(nomeAtt, valorAtt, descricaoAtt);

        //act
        repositorio.Atualizar(produtoBase);
        await repositorio.UnitOfWork.Commit(default);

        var result = await repositorio.ObterPorCodigoAsync(codigo, default);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(codigo, result.Codigo);
        Assert.Equal(nomeAtt, result.Nome);
        Assert.Equal(valorAtt, result.Valor);
        Assert.Equal(descricaoAtt, result.Descricao);
    }

    [Fact]
    public async Task Deletar_ProdutoValido_DeletadoComSucesso()
    {
        // Arrange
        var codigo = 5;
        var repositorio = _produtoRepositoryFixture.ObterProdutoRepository();
        var produto = _produtoRepositoryFixture.GerarProdutoValido(codigo);
        await repositorio.AdicionarAsync(produto, default);
        await repositorio.UnitOfWork.Commit(default);
        var produtoBase = await repositorio.ObterPorCodigoAsync(codigo, default);

        //act
        repositorio.Remover(produtoBase);
        await repositorio.UnitOfWork.Commit(default);

        var result = await repositorio.ObterPorCodigoAsync(codigo, default);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ObterPorCodigo_ProdutoValido_RetornaProdutoBase()
    {
        // Arrange
        var codigo = 6;
        var repositorio = _produtoRepositoryFixture.ObterProdutoRepository();
        var produto = _produtoRepositoryFixture.GerarProdutoValido(codigo);
        await repositorio.AdicionarAsync(produto, default);
        await repositorio.UnitOfWork.Commit(default);

        //act
        var result = await repositorio.ObterPorCodigoAsync(codigo, default);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(codigo, result.Codigo);
        Assert.Equal(produto.Nome, result.Nome);
        Assert.Equal(produto.Valor, result.Valor);
        Assert.Equal(produto.Descricao, result.Descricao);
    }

    [Fact]
    public async Task ObterTodos_ProdutosValidos_RetornaProdutosBase()
    {
        // Arrange
        var repositorio = _produtoRepositoryFixture.ObterProdutoRepository();
        var codigo = 7;
        var produto = _produtoRepositoryFixture.GerarProdutoValido(codigo, "produto 1");
        await repositorio.AdicionarAsync(produto, default);
        var codigo2 = 8;
        var produto2 = _produtoRepositoryFixture.GerarProdutoValido(codigo2, "produto 2");
        await repositorio.AdicionarAsync(produto, default);
        await repositorio.AdicionarAsync(produto2, default);
        await repositorio.UnitOfWork.Commit(default);

        //act
        var result = await repositorio.ObterTodosAsync(default);

        //Assert
        Assert.NotNull(result);
        Assert.Contains(codigo, result.Select(x => x.Codigo));
        Assert.Contains(codigo2, result.Select(x => x.Codigo));
        Assert.Contains(produto.Nome, result.Select(x => x.Nome));
        Assert.Contains(produto2.Nome, result.Select(x => x.Nome));
    }
}