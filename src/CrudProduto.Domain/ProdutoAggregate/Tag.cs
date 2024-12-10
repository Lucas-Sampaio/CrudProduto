using CrudProduto.Domain.SeedWork;

namespace CrudProduto.Domain.ProdutoAggregate;

public class Tag : Entity
{
    public const int DescricaoMaximo = 250;
    protected Tag()
    {
    }

    public Tag(string descricao)
    {
        Descricao = descricao;
    }

    public string Descricao { get; set; }
    public IReadOnlyCollection<Produto> Produtos { get; set; }
}