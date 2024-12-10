using CrudProduto.Domain.SeedWork;

namespace CrudProduto.Domain.ProdutoAggregate;

public class Produto : Entity, IAggregateRoot
{
    public const int DescricaoMaximo = 500;
    public const int NomeMaximo = 250;

    protected Produto()
    { }

    public Produto(int codigo, string nome, decimal valor, Tag tag, string? descricao = null)
    {
        Codigo = codigo;
        Nome = nome?.Trim();
        Valor = valor;
        Descricao = descricao?.Trim();
        Tag = tag;
    }

    public int Codigo { get; init; }
    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public Tag Tag { get; private set; }

    public void Alterar(string nome, decimal valor, string descricao, Tag categoria)
    {
        Nome = nome.Trim();
        Descricao = descricao?.Trim();
        Valor = valor;
        Tag = categoria;
    }
}