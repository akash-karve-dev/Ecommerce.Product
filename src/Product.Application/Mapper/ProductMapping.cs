using AutoMapper;
using Product.Application.Dtos.Input;
using Product.Domain.Enums;

namespace Product.Application.Mapper
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductDto, Domain.Models.Product>()
                .ForMember(dest => dest.Id,
                cfg => cfg.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Category,
                cfg => cfg.MapFrom(src => Enum.Parse<ProductCategory>(src.ProductCategory!)));
        }
    }
}