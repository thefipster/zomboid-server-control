using Microsoft.JSInterop;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Mods
{
    public partial class ModDiff
    {
        private IEnumerable<ModConfig> removed = new List<ModConfig>();
        private IEnumerable<ModConfig> added = new List<ModConfig>();
        private IEnumerable<ModConfig> same = new List<ModConfig>();

        public void Update()
        {
            var storedMods = ModStorage.Read();
            var serverMods = ModConfig.GetMods();

            setRemovedMods(storedMods, serverMods);
            setAddedMods(storedMods, serverMods);
            setSameMods(storedMods, added);

            StateHasChanged();
        }

        private async Task onShowModsClickAsync()
            => await JsRuntime.InvokeVoidAsync(JsMethods.ShowMods);

        private void setSameMods(IEnumerable<ModConfig> storedMods, IEnumerable<ModConfig> addedMods)
        {
            var sameIds = storedMods.Select(x => x.WorkshopId).Except(addedMods.Select(x => x.WorkshopId));
            var sameMods = new List<ModConfig>();
            foreach (var id in sameIds)
            {
                var mod = storedMods.FirstOrDefault(x => x.WorkshopId == id);
                sameMods.Add(mod);
            }
            same = sameMods;
        }

        private IEnumerable<string> setAddedMods(IEnumerable<ModConfig> storedMods, IEnumerable<ModConfig> serverMods)
        {
            var addedIds = storedMods.Select(x => x.WorkshopId).Except(serverMods.Select(x => x.WorkshopId));
            var addedMods = new List<ModConfig>();
            foreach (var id in addedIds)
            {
                var mod = storedMods.FirstOrDefault(x => x.WorkshopId == id);
                addedMods.Add(mod);
            }
            added = addedMods;
            return addedIds;
        }

        private void setRemovedMods(IEnumerable<ModConfig> storedMods, IEnumerable<ModConfig> serverMods)
        {
            var removedIds = serverMods.Select(x => x.WorkshopId).Except(storedMods.Select(x => x.WorkshopId));
            var removedMods = new List<ModConfig>();
            foreach (var id in removedIds)
            {
                var mod = serverMods.FirstOrDefault(x => x.WorkshopId == id);
                removedMods.Add(mod);
            }
            removed = removedMods;
        }
    }
}
