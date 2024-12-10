using CrudProduto.Domain.SeedWork;

namespace CrudProduto.Domain.ProdutoAggregate;

public interface IProdutoRepository : IRepository<Produto>
{
    ValueTask<List<Produto>> ObterTodosAsync(CancellationToken ct);

    ValueTask<List<Produto>> ObterPorTagAsync(string tag, CancellationToken ct);

    ValueTask<Produto?> ObterPorCodigoAsync(int codigo, CancellationToken ct);

    ValueTask AdicionarAsync(Produto produto, CancellationToken ct);

    void Remover(Produto produto);

    void Atualizar(Produto produto);
}