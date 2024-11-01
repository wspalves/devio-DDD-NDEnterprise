using AutoMapper;
using DDDNerdStore.Catalogo.Application.DTOs;
using DDDNerdStore.Catalogo.Domain;
using DDDNerdStore.Catalogo.Domain.Entities;
using DDDNerdStore.Catalogo.Domain.ValueObjects;

namespace DDDNerdStore.Catalogo.Application.AutoMapper;

public class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        CreateMap<ProdutoDTO, Produto>()
            .ConstructUsing(p => new Produto(p.Nome, p.Descricao, p.Valor, p.CategoriaId,
                new Dimensoes(p.Altura, p.Largura, p.Profundidade), p.DataCadastro, p.Imagem, p.Ativo));

        CreateMap<CategoriaDTO, Categoria>()
            .ConstructUsing(c => new Categoria(c.Nome, c.Codigo));
    }
}