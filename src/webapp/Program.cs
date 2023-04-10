using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Services;
using TheFipster.Zomboid.ServerControl.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddJsonFile(Const.IniSettingsFilename);
        builder.AddJsonFile(Const.SandboxSettingsFilename);

        builder.AddConfig<AppSettings>();
        builder.AddConfigSection<IniSettings>(IniSettings.SectionName);
        builder.AddConfigSection<SandboxSettings>(SandboxSettings.SectionName);

        builder.Services.Configure<IniSettings>(builder.Configuration.GetSection(IniSettings.SectionName));

        builder.Services.AddSingleton<IniFileService>();
        builder.Services.AddSingleton<IniSettingsService>();
        builder.Services.AddSingleton<ModConfigService>();
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