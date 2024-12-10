using CrudProduto.Application.Shared;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutoPorTag;

public class ObterProdutosPorTagOutput : OutputModel
{
    public List<ProdutoResponse> Produtos { get; set; }
}