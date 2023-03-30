using zomboid_server_control.Data;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Configuration.AddEnvironmentVariables();

        var settings = new AppSettings();
        builder.Configuration.Bind(settings);
        builder.Services.AddSingleton(settings);

        builder.Services.AddSingleton<ServerConfigService>();
        builder.Services.AddSingleton<ModStorageService>();
        builder.Services.AddSingleton<DockerInteropService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
            app.UseExceptionHandler("/Error");

        app.UseStaticFiles();
        app.UseRouting();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
        app.Run();
    }
}