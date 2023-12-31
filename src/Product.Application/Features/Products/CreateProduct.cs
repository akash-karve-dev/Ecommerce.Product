﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Product.Application.Contracts;
using Product.Application.Dtos.Input;
using Product.Domain.Enums;

namespace Product.Application.Features.Products
{
    public class CreateProduct
    {
        public class Command : CreateProductDto, IRequest<Unit>
        {
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty();

                RuleFor(x => x.ProductCategory)
                    .IsEnumName(typeof(ProductCategory), false);
            }
        }

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