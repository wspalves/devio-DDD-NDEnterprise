using System.ComponentModel.DataAnnotations;

namespace DDDNerdStore.Catalogo.Application.DTOs;

public class ProdutoDTO
{
    [Key] 
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Preencha o Campo {0}")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Preencha o Campo {0}")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Preencha o Campo {0}")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "Preencha o Campo {0}")]
    public DateTime DataCadastro { get; set; }

    [Required(ErrorMessage = "Preencha o Campo {0}")]
    public string? Imagem { get; set; }

    [Required(ErrorMessage = "Preencha o Campo {0}")]
    public int QuantidadeEstoque { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter valor de no minimo {1}")]
    [Required(ErrorMessage = "Preencha o Campo {0}")]
    public int Altura { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter valor de no minimo {1}")]
    [Required(ErrorMessage = "Preencha o Campo {0}")]
    public int Largura { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter valor de no minimo {1}")]
    [Required(ErrorMessage = "Preencha o Campo {0}")]
    public int Profundidade { get; set; }

    [Required(ErrorMessage = "Preencha o Campo {0}")]
    public bool Ativo { get; set; }

    public Guid CategoriaId { get; set; }

    public IEnumerable<CategoriaDTO>? Categorias { get; set; }
}