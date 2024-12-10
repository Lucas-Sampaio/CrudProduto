using CrudProduto.Domain.ProdutoAggregate;
using CrudProduto.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace CrudProduto.Infra;

public class CrudProdutoContext : DbContext, IUnitOfWork
{
    public CrudProdutoContext(DbContextOptions<CrudProdutoContext> options) : base(options) { }
    public DbSet<Produto> Produtos { get; set; }

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
        //seta as colunas string pra varchar(250)
        var propriedades = modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetProperties().Where(y => y.ClrType == typeof(string)));
        foreach (var property in propriedades)
        {
            property.SetColumnType("varchar(250)");
        }
        // pega as configurações das entidades
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrudProdutoContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async ValueTask<bool> Commit(CancellationToken ct) =>
         await base.SaveChangesAsync(ct) > 0;
}