using DDDNerdStore.Catalogo.Application.Services;
using DDDNerdStore.Catalogo.Data;
using DDDNerdStore.Catalogo.Data.Repository;
using DDDNerdStore.Catalogo.Domain;
using DDDNerdStore.Catalogo.Domain.Events;
using DDDNerdStore.Core.Bus;
using MediatR;

namespace DDDNerdStore.WebApp.MVC.Setup;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IMediatrHandler, MediatrHandler>();

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoAppService, ProdutoAppService>();
        services.AddScoped<IEstoqueService, EstoqueService>();
        services.AddScoped<CatalogoContext>();

        services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();
    }
}