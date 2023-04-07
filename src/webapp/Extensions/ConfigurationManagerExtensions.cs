using TheFipster.Zomboid.ServerControl.Config;

namespace TheFipster.Zomboid.ServerControl.Extensions
{
    public static class ConfigurationManagerExtensions
    {
        public static WebApplicationBuilder AddJsonFile(this WebApplicationBuilder builder, string filename)
        {
            var rootPath = builder.Environment.ContentRootPath;
            var appsettingsPath = Path.Combine(rootPath, filename);
            builder.Configuration.AddJsonFile(appsettingsPath, true);

            return builder;
        }
    }
}
