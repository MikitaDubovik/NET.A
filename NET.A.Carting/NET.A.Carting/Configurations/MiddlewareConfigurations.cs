using NET.A.Carting.Middlewares;

namespace NET.A.Carting.Configurations
{
    public static class MiddlewareConfigurations
    {
        public static void AddMiddlewares(this IServiceCollection services)
        {
            services.AddTransient<IdentityTokenLoggerMiddleware>();
        }
    }
}
