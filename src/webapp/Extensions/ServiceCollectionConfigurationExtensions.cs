namespace TheFipster.Zomboid.ServerControl.Extensions
{
    public static class ServiceCollectionConfigurationExtensions
    {
        public static WebApplicationBuilder AddConfig<T>(this WebApplicationBuilder builder) where T : class, new()
        {
            var config = new T();
            builder.Configuration.Bind(config);
            builder.Services.AddSingleton(config);

            return builder;
        }

        public static WebApplicationBuilder AddConfigSection<T>(this WebApplicationBuilder builder, string section) where T : class, new()
        {
            var config = new T();
            builder.Configuration.GetSection(section).Bind(config);
            builder.Services.AddSingleton(config);

            return builder;
        }
    }
}
