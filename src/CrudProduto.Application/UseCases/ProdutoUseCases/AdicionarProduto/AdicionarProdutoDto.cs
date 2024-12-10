namespace CrudProduto.Application.UseCases.ProdutoUseCases.AdicionarProduto;

public class AdicionarProdutoDto
{
    /// <summary>
    /// Representa um codigo unico do produto
    /// </summary>
    public int Codigo { get; set; }

    /// <summary>
    /// Nome do produto
    /// </summary>

    public string Nome { get; set; }

    /// <summary>
    /// Valor do produto
    /// </summary>

    public decimal Valor { get; set; }

    /// <summary>
    /// descricao do produto
    /// </summary>

    public string Descricao { get; set; }

    /// <summary>
    /// Tag de categoria
    /// </summary>
    public string Tag { get; set; }
}