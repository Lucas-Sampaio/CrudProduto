using CrudProduto.Application.UseCases.ProdutoUseCases.AdicionarProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.AtualizarProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.DeletarProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.ObterProduto;
using CrudProduto.Application.UseCases.ProdutoUseCases.ObterProdutos;
using CrudProduto.Domain.ProdutoAggregate;
using CrudProduto.Infra;
using CrudProduto.Infra.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrudProduto.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        //EF
        var connection = configuration.GetConnectionString("SQLConnection");
        services.AddDbContext<CrudProdutoContext>(options =>
        {
            options
            .UseSqlServer(connection, config => config.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null));
        });

        //usecases
        services.AddScoped<IRequestHandler<AdicionarProdutoInput, AdicionarProdutoOutput>, AdicionarProdutoHandler>();
        services.AddScoped<IRequestHandler<ObterProdutoInput, ObterProdutoOutput>, ObterProdutoHandler>();
        services.AddScoped<IRequestHandler<ObterProdutosInput, ObterProdutosOutput>, ObterProdutosHandler>();
        services.AddScoped<IRequestHandler<DeletarProdutoInput, DeletarProdutoOutput>, DeletarProdutoHandler>();
        services.AddScoped<IRequestHandler<AtualizarProdutoInput, AtualizarProdutoOutput>, AtualizarProdutoHandler>();

        //repositorios
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
    }
}