using Microsoft.Extensions.DependencyInjection;

namespace Product.Application
{
    public static class Builder
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Builder).Assembly);
            });

            services.AddAutoMapper(typeof(Builder));

            return services;
        }
    }
}