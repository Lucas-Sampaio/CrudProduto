using CrudProduto.Domain.ProdutoAggregate;
using CrudProduto.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace CrudProduto.Infra.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    public ProdutoRepository(CrudProdutoContext context) => _context = context;

    private readonly CrudProdutoContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public async ValueTask AdicionarAsync(Produto produto, CancellationToken ct) =>
         await _context.Produtos.AddAsync(produto, ct);

    public void Atualizar(Produto produto) =>
        _context.Produtos.Update(produto);

    public async ValueTask<Produto?> ObterPorCodigoAsync(int codigo, CancellationToken ct) =>
     await _context.Produtos.FirstOrDefaultAsync(x => x.Codigo == codigo, ct);

    public ValueTask<List<Produto>> ObterPorTagAsync(string tag, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<List<Produto>> ObterTodosAsync(CancellationToken ct) =>
        await _context.Produtos.AsNoTrackingWithIdentityResolution().ToListAsync(ct);

    public void Remover(Produto produto)
    {
        if (produto is not null)
            _context.Produtos.Remove(produto);
    }
}