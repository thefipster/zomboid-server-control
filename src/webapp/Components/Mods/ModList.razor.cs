using Microsoft.JSInterop;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Mods
{
    public partial class ModList
    {
        public IList<ModConfig> Mods { get; set; } = new List<ModConfig>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Mods = ModStorage.Get().ToList();
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
                await JsRuntime.InvokeVoidAsync(JsMethods.SetupModsDragAndDrop);
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
            StateHasChanged();
        }

        private bool validate(ModConfig mod) => Mods.All(x => x.WorkshopId != mod.WorkshopId);

        private void onRemoveClick(string workshopId)
        {
            var mod = Mods.FirstOrDefault(x => x.WorkshopId == workshopId);
            if (mod != null)
            {
                ModArchive.Insert(mod);
                Mods.Remove(mod);
            }
        }
    }
}
