using DDDNerdStore.Catalogo.Application.Services;
using DDDNerdStore.Catalogo.Data;
using DDDNerdStore.Catalogo.Data.Repository;
using DDDNerdStore.Catalogo.Domain;
using DDDNerdStore.Catalogo.Domain.Events;
using DDDNerdStore.Core.Communication.Mediator;
using DDDNerdStore.Core.Messages.CommonMessages.Notifications;
using DDDNerdStore.Vendas.Application.Commands;
using DDDNerdStore.Vendas.Application.Events;
using DDDNerdStore.Vendas.Data;
using DDDNerdStore.Vendas.Data.Repository;
using DDDNerdStore.Vendas.Domain.Interfaces;
using MediatR;

namespace DDDNerdStore.WebApp.MVC.Setup;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        // Mediator
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        // Notifications
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Catalogo
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IProdutoAppService, ProdutoAppService>();
        services.AddScoped<IEstoqueService, EstoqueService>();
        services.AddScoped<CatalogoContext>();
        services.AddScoped<INotificationHandler<ProdutoAbaixoEstoqueEvent>, ProdutoEventHandler>();

        // Vendas
        services.AddScoped<VendasContext>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, bool>, PedidoCommandHandler>();
        
        services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoAtualizadoEvent>, PedidoEventHandler>();
        services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, PedidoEventHandler>();
    }
}