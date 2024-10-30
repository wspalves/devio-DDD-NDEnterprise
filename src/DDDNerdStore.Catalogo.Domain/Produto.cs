using DDDNerdStore.Core.DomainObjects;

namespace DDDNerdStore.Catalogo.Domain;

public class Produto : Entity, IAggregateRoot
{
    public Produto(string nome, string descricao, decimal valor, Guid categoriaId, Dimensoes dimensoes,
        DateTime dataCadastro,
        string? imagem, bool ativo)
    {
        Nome = nome;
        Descricao = descricao;
        Valor = valor;
        DataCadastro = dataCadastro;
        Imagem = imagem;
        Ativo = ativo;
        CategoriaId = categoriaId;
        Dimensoes = dimensoes;

        Validar();
    }

    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public string? Imagem { get; private set; }
    public int QuantidadeEstoque { get; private set; }
    public bool Ativo { get; private set; }

    public Guid CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; }

    public Dimensoes Dimensoes { get; private set; }

    public void Ativar() => Ativo = true;
    public void Desativar() => Ativo = false;

    public void AlterarCategoria(Categoria categoria)
    {
        Categoria = categoria;
        CategoriaId = categoria.Id;
    }

    public void AlterarDescricao(string descricao)
    {
        Validacoes.ValidarSeVazio(descricao, "A descrição do Produto não pode ser vazia.");
        Descricao = descricao;
    }

    public void DebitarEstoque(int quantidade)
    {
        if (quantidade < 0) quantidade *= -1;
        if (!PossuiEstoque(quantidade)) throw new DomainException("Estoque insuficiente.");
        QuantidadeEstoque -= quantidade;
    }

    public void ReporEstoque(int quantidade)
    {
        QuantidadeEstoque += quantidade;
    }

    public bool PossuiEstoque(int quantidade)
    {
        return QuantidadeEstoque >= quantidade;
    }

    public void Validar()
    {
        Validacoes.ValidarSeVazio(Nome, "O campo Nome do Produto não pode ser vazio.");
        Validacoes.ValidarSeVazio(Descricao, "O campo Descricao do Produto não pode ser vazio.");
        Validacoes.ValidarSeIgual(CategoriaId, Guid.Empty, "O campo CategoriaId do Produto não pode estar vazio.");
        Validacoes.ValidarSeMenorIgualMinimo(Valor, 0, "O campo Valor do Produto deve ser maior que zero.");
        Validacoes.ValidarSeVazio(Imagem, "O campo Imagem do Produto não deve ser vazio.");
    }
}