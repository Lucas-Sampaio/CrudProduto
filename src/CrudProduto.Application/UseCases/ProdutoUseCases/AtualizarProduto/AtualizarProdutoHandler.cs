using CrudProduto.Domain.ProdutoAggregate;
using MediatR;

namespace CrudProduto.Application.UseCases.ProdutoUseCases.AtualizarProduto;

public class AtualizarProdutoHandler(IProdutoRepository produtoRepository) : IRequestHandler<AtualizarProdutoInput, AtualizarProdutoOutput>
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));

    public async Task<AtualizarProdutoOutput> Handle(AtualizarProdutoInput request, CancellationToken cancellationToken)
    {
        var outputModel = new AtualizarProdutoOutput();
        var produtoDto = request.Produto;
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

        var tag = await _produtoRepository.ObterTagOuAdicionarAsync(request.Produto.Tag, cancellationToken);

        produto.Alterar(request.Produto.Nome, request.Produto.Valor, request.Produto.Descricao, tag);

        _produtoRepository.Atualizar(produto);
        await _produtoRepository.UnitOfWork.Commit(cancellationToken);

        return new AtualizarProdutoOutput();
    }
}