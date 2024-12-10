using CrudProduto.Domain.ProdutoAggregate;
using CrudProduto.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace CrudProduto.Infra;

public class CrudProdutoContext : DbContext, IUnitOfWork
{
    public CrudProdutoContext(DbContextOptions<CrudProdutoContext> options) : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //usado so em debug para obter erros mais detalhados
#if DEBUG
        optionsBuilder.EnableDetailedErrors();
#endif

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // pega as configurações das entidades
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrudProdutoContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public void CriarBanco()
    {
        try
        {
            _ = Database.EnsureCreated();
            Database.Migrate();
        }
        catch (Exception ex)
        {
            _ = ex;
        }

    }
    public async ValueTask<bool> Commit(CancellationToken ct) =>
         await base.SaveChangesAsync(ct) > 0;
}