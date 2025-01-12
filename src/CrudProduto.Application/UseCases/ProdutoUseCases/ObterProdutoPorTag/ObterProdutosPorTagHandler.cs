using CrudProduto.Domain.ProdutoAggregate;
using MediatR;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutoPorTag;

public class ObterProdutosPorTagHandler(IProdutoRepository produtoRepository) : IRequestHandler<ObterProdutosPorTagInput, ObterProdutosPorTagOutput>
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));

    public async Task<ObterProdutosPorTagOutput> Handle(ObterProdutosPorTagInput request, CancellationToken cancellationToken)
    {
        var outputModel = new ObterProdutosPorTagOutput();

        if (!request.EhValido())
        {
            outputModel.AdicionarErros(request.ValidationResult.Errors);
            return outputModel;
        }
        var produtos = await _produtoRepository.ObterPorTagAsync(request.Tag, cancellationToken);
        outputModel.Produtos = produtos.ConvertAll(x => new Shared.ProdutoResponse
        {
            Codigo = x.Codigo,
            Descricao = x.Descricao,
            Nome = x.Nome,
            Valor = x.Valor,
            Tag = x.Tag?.Descricao
        });
        return outputModel;
    }
}