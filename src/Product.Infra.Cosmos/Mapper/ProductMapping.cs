using AutoMapper;
using Product.Application;
using Product.Infra.Cosmos.Models;
using System.Text.Json;

namespace Product.Infra.Cosmos.Mapper
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Domain.Models.Product, CosmosProduct>()
                .ForMember(dest => dest.PartitionKey,
                cfg => cfg.MapFrom(src => src.Id.ToString()));

            CreateMap<Domain.Models.Product, OutboxEntity>()
                .ForMember(dest => dest.Id,
                cfg => cfg.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.EventType,
                cfg => cfg.MapFrom(src => "ProductCreated"))
            .ForMember(dest => dest.EventPayload,
                cfg => cfg.MapFrom(src => JsonSerializer.Serialize(src, SerializerOptions.DefaultSerializerOptions())))
                .ForMember(dest => dest.PartitionKey,
                cfg => cfg.MapFrom(src => src.Id.ToString()));
        }
    }
}