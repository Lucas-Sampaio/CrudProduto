namespace CrudProduto.Application.Shared;

public class ProdutoResponse
{
    public int Codigo { get; set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Valor { get; set; }
}