using Microsoft.JSInterop;
using TheFipster.Zomboid.ServerControl.Components.Mods;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Pages
{
    public partial class Mods
    {
        ModList? modlist;
        LogList? loglist;
        ModDiff? modDiff;

        private async Task addModAsync(ModConfig mod)
        {
            if (modlist == null) return;

            await modlist.AddModAsync(mod);
        }

        private async Task restartConfirmedAsync()
        {
            if (modlist == null || loglist == null) return;

            await JsRuntime.InvokeVoidAsync(JsMethods.ShowLogs);
            ServerConfig.SetMods(new ModCollection(modlist.Mods));
            await DockerInterop.RestartAsync();
            loglist.Start();
        }

        private async Task diffAsync()
        {
            if (modDiff == null) return;

            modDiff.Update();
            await JsRuntime.InvokeVoidAsync(JsMethods.ShowModDiff);
        }
    }
}
