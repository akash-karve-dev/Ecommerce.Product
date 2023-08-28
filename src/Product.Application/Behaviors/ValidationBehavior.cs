using FluentValidation;
using MediatR;

namespace Product.Application.Behaviors
{
    internal class ValidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            /*
             * TODO : NEED TO ADD LOGGING
             */

            var context = new ValidationContext<TRequest>(request);

            var errors = _validators
                  .Select(p => p.Validate(context))
                  .SelectMany(p => p.Errors)
                  .ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            return next();
        }
    }
}