using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Dtos.Input;
using Product.Application.Features.Products;

namespace Product.Presentation.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            if (productDto == null)
            {
                throw new BadHttpRequestException("Request cannot be serialized");
            }

            var command = new CreateProduct.Command
            {
                Name = productDto.Name,
                ProductCategory = productDto.ProductCategory
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}