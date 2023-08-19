namespace Product.Presentation.Api
{
    // TODO : REPLACE BY PROBLEM DETAILS
    public class ExceptionShieldingMiddleware
    {
        private readonly ILogger<ExceptionShieldingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionShieldingMiddleware(
            ILogger<ExceptionShieldingMiddleware> logger,
            RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}