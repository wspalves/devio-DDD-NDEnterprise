using DDDNerdStore.Core.DomainObjects;

namespace DDDNerdStore.Catalogo.Domain.Entities;

public class Categoria : Entity
{
    protected Categoria()
    {
    }

    public Categoria(string nome, int codigo)
    {
        Nome = nome;
        Codigo = codigo;

        Validar();
    }

    public string Nome { get; private set; }
    public int Codigo { get; private set; }
    public ICollection<Produto> Produtos { get; set; }

    public override string ToString()
    {
        return $"{Codigo} - {Nome}";
    }

    public void Validar()
    {
        Validacoes.ValidarSeVazio(Nome, "O campo Nome da Categoria não pode ser vazio.");
        Validacoes.ValidarSeIgual(Codigo, 0, "O campo Codigo da Categoria não pode ser 0.");
    }
}