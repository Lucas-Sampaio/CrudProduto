using CrudProduto.Domain.ProdutoAggregate;

namespace CrudProduto.Application.Shared;

public class ProdutoResponse
{
    public ProdutoResponse()
    {
    }

    public ProdutoResponse(int codigo, string nome, decimal valor, string tag, string? descricao)
    {
        Codigo = codigo;
        Nome = nome;
        Descricao = descricao;
        Valor = valor;
        Tag = tag;
    }

    public int Codigo { get; set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Valor { get; set; }
    public string Tag { get; set; }
}