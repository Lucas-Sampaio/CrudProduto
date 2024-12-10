using CrudProduto.Application.Shared;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutos;

public class ObterProdutosInput : InputModel<ObterProdutosOutput>
{
    public override bool EhValido()
    {
        return true;
    }
}