using CrudProduto.Application.Shared;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutos;

public class ObterProdutosOutput : OutputModel
{
    public List<ProdutoResponse> Produtos { get; set; }
}