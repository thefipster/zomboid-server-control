using TheFipster.Zomboid.ServerControl.Components.Mods;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Pages
{
    public partial class Mods
    {
        ModList? modlist;

        private async Task addModAsync(ModConfig mod)
        {
            ModArchive.Insert(mod);

            if (modlist == null) return;
            await modlist.AddModAsync(mod);
        }
    }
}
