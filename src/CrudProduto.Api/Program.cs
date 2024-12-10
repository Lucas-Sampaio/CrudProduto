using CrudProduto.Api.Configuration;
using CrudProduto.Application.UseCases.ProdutoUseCases.AdicionarProduto;
using CrudProduto.Infra;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace CrudProduto.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssembly(typeof(AdicionarProdutoValidation).Assembly);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerConfiguration();
        builder.Services.RegisterServices(builder.Configuration);
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        var app = builder.Build();

        app.UseSwaggerConfiguration();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        InicializarBanco(app.Services);
        app.Run();
    }

    //cria banco na inicializacao da aplicacao
    private static async void InicializarBanco(IServiceProvider serviceProvider)
    {
        using IServiceScope serviceScope = serviceProvider.CreateScope();

        IServiceProvider provider = serviceScope.ServiceProvider;

        var crudProdutoContext = provider.GetRequiredService<CrudProdutoContext>();

        //cria o banco e as tabelas
        crudProdutoContext.CriarBanco();
    }
}