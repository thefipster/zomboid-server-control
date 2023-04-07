using Microsoft.JSInterop;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Mods
{
    public partial class ModList
    {
        public IList<ModConfig> Mods { get; set; } = new List<ModConfig>();

        protected override void OnInitialized()
            => Mods = ModStorage.Read().ToList();

        protected override async void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                var dotNetReference = DotNetObjectReference.Create(this);
                await JsRuntime.InvokeVoidAsync(JsMethods.SyncInstances, dotNetReference);
                await JsRuntime.InvokeVoidAsync(JsMethods.OnAfterRender);
            }
        }

        [JSInvokable(JsInvokables.SyncMods)]
        public void SyncMods(ModConfig[] reorderedMods)
        {
            syncModOrder(reorderedMods);
            Mods = Mods.OrderBy(x => x.Order).ToList();
            ModStorage.Write(Mods);
        }

        public async Task AddModAsync(ModConfig mod)
        {
            if (!validate(mod))
            {
                await JsRuntime.InvokeVoidAsync(JsMethods.Alert, Messages.DuplicateMod);
                return;
            }

            mod.Order = Mods.Count;
            Mods.Add(mod);
            ModStorage.Write(Mods);
            StateHasChanged();
        }

        private bool validate(ModConfig mod) => Mods.All(x => x.WorkshopId != mod.WorkshopId);

        private void onRemoveClick(string workshopId)
        {
            var mod = Mods.FirstOrDefault(x => x.WorkshopId == workshopId);
            if (mod != null)
            {
                ModStorage.Archive(mod);
                Mods.Remove(mod);
            }

            ModStorage.Write(Mods);
        }

        private void syncModOrder(ModConfig[] reorderedMods)
        {
            for (int i = 0; i < reorderedMods.Count(); i++)
            {
                var workshopId = reorderedMods[i].WorkshopId;
                Mods.First(x => x.WorkshopId == workshopId).Order = i;
            }
        }
    }
}
