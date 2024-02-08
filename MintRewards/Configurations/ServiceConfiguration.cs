/*namespace MintRewards.Configurations
{

    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });
            return services;
        }

        public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            return services;
        }

    }
}
*/