using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Services;
using TheFipster.Zomboid.ServerControl.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.Configuration.AddEnvironmentVariables();
        //builder.AddJsonFile(Const.AppSettingsFilename);

        builder.AddConfig<AppSettings>();
        builder.AddConfigSection<List<SettingsEntry>>(Const.IniSettingsKey);

        builder.Services.AddSingleton<ServerConfigService>();
        builder.Services.AddSingleton<ModStorageService>();
        builder.Services.AddSingleton<DockerInteropService>();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        builder.WebHost.UseStaticWebAssets();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
            app.UseExceptionHandler("/error");

        app.UseStaticFiles();
        app.UseRouting();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
        app.Run();
    }
}