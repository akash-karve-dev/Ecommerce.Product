using AutoMapper;
using MediatR;
using Product.Application.Contracts;
using Product.Application.Dtos.Input;

namespace Product.Application.Features.Products
{
    public class CreateProduct
    {
        public class Command : CreateProductDto, IRequest<Unit>
        {
        }

        // TODO: FLUENT VALIDATION

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public Handler(
                IProductRepository productRepository,
                IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var domainProduct = _mapper.Map<Domain.Models.Product>(request);

                await _productRepository.AddAsync(domainProduct);

                return Unit.Value;
            }
        }
    }
}