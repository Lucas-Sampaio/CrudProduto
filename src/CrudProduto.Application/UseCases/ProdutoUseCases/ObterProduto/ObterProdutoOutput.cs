using CrudProduto.Application.Shared;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.ObterProduto;

public class ObterProdutoOutput : OutputModel
{
    public ObterProdutoOutput()
    {
    }

    public ProdutoResponse Produto { get; set; }
}