using CrudProduto.Domain.ProdutoAggregate;
using MediatR;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.DeletarProduto;

public class DeletarProdutoHandler(IProdutoRepository produtoRepository) : IRequestHandler<DeletarProdutoInput, DeletarProdutoOutput>
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));

    public async Task<DeletarProdutoOutput> Handle(DeletarProdutoInput request, CancellationToken cancellationToken)
    {
        var outputModel = new DeletarProdutoOutput();

        if (!request.EhValido())
        {
            outputModel.AdicionarErros(request.ValidationResult.Errors);
            return outputModel;
        }
        var produto = await _produtoRepository.ObterPorCodigoAsync(request.Codigo, cancellationToken);

        _produtoRepository.Remover(produto);
        await _produtoRepository.UnitOfWork.Commit(cancellationToken);
        return new DeletarProdutoOutput();
    }
}