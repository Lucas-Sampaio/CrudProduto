using CrudProduto.Domain.ProdutoAggregate;
using MediatR;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutos;

public class ObterProdutosHandler(IProdutoRepository produtoRepository) : IRequestHandler<ObterProdutosInput, ObterProdutosOutput>
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));

    public async Task<ObterProdutosOutput> Handle(ObterProdutosInput request, CancellationToken cancellationToken)
    {
        var produtos = await _produtoRepository.ObterTodosAsync(cancellationToken);

        return new ObterProdutosOutput
        {
            Produtos = produtos.ConvertAll(x => new Shared.ProdutoResponse
            {
                Codigo = x.Codigo,
                Descricao = x.Descricao,
                Nome = x.Nome,
                Valor = x.Valor,
                Tag = x.Tag?.Descricao
            }),
        };
    }
}