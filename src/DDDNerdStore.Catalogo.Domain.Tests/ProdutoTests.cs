using DDDNerdStore.Core.DomainObjects;

namespace DDDNerdStore.Catalogo.Domain.Tests;

public class ProdutoTests
{
    [Fact]
    public void Produto_Validar_ValidacoesDevemRetornarExceptions()
    {
        var ex = Assert.Throws<DomainException>(() => new Produto(string.Empty, "Descricao", 15, Guid.NewGuid(),
            new Dimensoes(2, 2, 2), DateTime.Now, "urldaimagem", true));

        Assert.Equal("O campo Nome do Produto não pode ser vazio.", ex.Message);

        ex = Assert.Throws<DomainException>(() => new Produto("Nome do produto", string.Empty, 15, Guid.NewGuid(),
            new Dimensoes(2, 2, 2), DateTime.Now, "urldaimagem", true));

        Assert.Equal("O campo Descricao do Produto não pode ser vazio.", ex.Message);

        ex = Assert.Throws<DomainException>(() => new Produto("Nome do produto", "Descricao", 0, Guid.NewGuid(),
            new Dimensoes(2, 2, 2), DateTime.Now, "urldaimagem", true));

        Assert.Equal("O campo Valor do Produto deve ser maior que zero.", ex.Message);

        ex = Assert.Throws<DomainException>(() => new Produto("Nome do produto", "Descricao", 15, Guid.Empty,
            new Dimensoes(2, 2, 2), DateTime.Now, "urldaimagem", true));

        Assert.Equal("O campo CategoriaId do Produto não pode estar vazio.", ex.Message);
    }
}