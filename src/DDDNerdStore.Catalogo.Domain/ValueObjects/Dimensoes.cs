using DDDNerdStore.Core.DomainObjects;

namespace DDDNerdStore.Catalogo.Domain;

public class Dimensoes
{
    public decimal Altura { get; private set; }
    public decimal Largura { get; private set; }
    public decimal Profundidade { get; private set; }

    public Dimensoes(decimal altura, decimal largura, decimal profundidade)
    {
        Validacoes.ValidarSeMenorIgualMinimo(altura, 1, "O campo Altura deve ser maior que 1cm.");
        Validacoes.ValidarSeMenorIgualMinimo(largura, 1, "O campo Largura deve ser maior que 1cm.");
        Validacoes.ValidarSeMenorIgualMinimo(profundidade, 1, "O campo Profundidade deve ser maior que 1cm.");

        Altura = altura;
        Largura = largura;
        Profundidade = profundidade;
    }

    public string DescricaoFormatada() => $"LXAXP {Largura} x {Altura} x {Profundidade}";

    public override string ToString() => DescricaoFormatada();
}