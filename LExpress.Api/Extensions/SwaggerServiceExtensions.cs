namespace LExpress.Api.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI();
            return builder;
        }
    }
}
