using Product.Application;
using Product.Infra.Cosmos;
using Product.Presentation.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        #region CONFIGURE SERVICES

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // SWAGGER
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddApplication();
        builder.Services.AddCosmosDb(builder.Configuration);

        #endregion CONFIGURE SERVICES

        #region CONFIGURE

        var app = builder.Build();

        app.UseMiddleware<ExceptionShieldingMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        //app.UseAuthorization();

        app.MapControllers();

        app.Run();

        #endregion CONFIGURE
    }
}