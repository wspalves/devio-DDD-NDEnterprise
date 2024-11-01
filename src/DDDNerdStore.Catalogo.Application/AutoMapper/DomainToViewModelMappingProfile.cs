using AutoMapper;
using DDDNerdStore.Catalogo.Application.DTOs;
using DDDNerdStore.Catalogo.Domain;
using DDDNerdStore.Catalogo.Domain.Entities;

namespace DDDNerdStore.Catalogo.Application.AutoMapper;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Produto, ProdutoDTO>()
            .ForMember(d => d.Largura, o => o.MapFrom(s => s.Dimensoes.Largura))
            .ForMember(d => d.Altura, o => o.MapFrom(s => s.Dimensoes.Altura))
            .ForMember(d => d.Profundidade, o => o.MapFrom(s => s.Dimensoes.Profundidade));

        CreateMap<Categoria, CategoriaDTO>();
    }
}