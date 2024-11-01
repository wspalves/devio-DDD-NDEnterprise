using FluentValidation;

namespace DDDNerdStore.Vendas.Application.Commands.Validator;

public class AdicionarItemPedidoValidator : AbstractValidator<AdicionarItemPedidoCommand>
{
    public AdicionarItemPedidoValidator()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do produto inválido");

        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("Nome do produto não informado");

        RuleFor(c => c.Quantidade)
            .GreaterThan(0)
            .WithMessage("A quantidade minima do item é 1");

        RuleFor(c => c.Quantidade)
            .LessThan(20)
            .WithMessage("A quantidade máxima de itens é 20");

        RuleFor(c => c.ValorUnitario)
            .GreaterThan(0)
            .WithMessage("O valor do item precisa ser maior que 0");
    }
}