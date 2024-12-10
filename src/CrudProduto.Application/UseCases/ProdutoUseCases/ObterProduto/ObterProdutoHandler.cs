using CrudProduto.Domain.ProdutoAggregate;
using MediatR;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.ObterProduto;

public class ObterProdutoHandler(IProdutoRepository produtoRepository) : IRequestHandler<ObterProdutoInput, ObterProdutoOutput>
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));

    public async Task<ObterProdutoOutput> Handle(ObterProdutoInput request, CancellationToken cancellationToken)
    {
        var outputModel = new ObterProdutoOutput();

        if (!request.EhValido())
        {
            outputModel.AdicionarErros(request.ValidationResult.Errors);
            return outputModel;
        }
        var produto = await _produtoRepository.ObterPorCodigoAsync(request.Codigo, cancellationToken);
        if (produto is null)
        {
            outputModel.AdicionarErro("Nao existe um produto com esse codigo");
            return outputModel;
        }

        return new ObterProdutoOutput(produto.Codigo, produto.Nome, produto.Valor, produto.Tag?.Descricao, produto.Descricao);
    }
}