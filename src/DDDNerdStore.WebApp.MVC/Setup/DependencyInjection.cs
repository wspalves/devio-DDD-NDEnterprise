using DDDNerdStore.Catalogo.Application.Services;
using DDDNerdStore.Catalogo.Data;
using DDDNerdStore.Catalogo.Data.Repository;
using DDDNerdStore.Catalogo.Domain;
using DDDNerdStore.Catalogo.Domain.Events;
using DDDNerdStore.Core.Bus;
using DDDNerdStore.Vendas.Application.Commands;
using DDDNerdStore.Vendas.Data;
using DDDNerdStore.Vendas.Data.Repository;
using DDDNerdStore.Vendas.Domain.Interfaces;
using MediatR;

namespace DDDNerdStore.WebApp.MVC.Setup;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoAppService, ProdutoAppService>();
        services.AddScoped<IEstoqueService, EstoqueService>();
        services.AddScoped<CatalogoContext>();

        services.AddScoped<VendasContext>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

        services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
    }
}