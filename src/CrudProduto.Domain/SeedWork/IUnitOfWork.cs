namespace CrudProduto.Domain.SeedWork;

public interface IUnitOfWork
{
    ValueTask<bool> Commit(CancellationToken ct);
}