using CrudProduto.Domain.SeedWork;

namespace CrudProduto.Domain.ProdutoAggregate;

public class Produto : Entity, IAggregateRoot
{
    public const int DescricaoMaximo = 500;
    public const int NomeMaximo = 250;

    protected Produto()
    { }

    public Produto(int codigo, string nome, decimal valor, string? descricao = null)
    {
        Codigo = codigo;
        Nome = nome;
        Valor = valor;
        Descricao = descricao;
    }

    public int Codigo { get; init; }
    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public decimal Valor { get; private set; }

    public void Alterar(string nome, decimal valor, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
        Valor = valor;
    }
}