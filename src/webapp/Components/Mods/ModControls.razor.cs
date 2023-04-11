using Microsoft.JSInterop;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Data;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Mods
{
    public partial class ModControls
    {
        private readonly ConfirmTimer timer = new(2000, 2);

        public string btnText = Messages.DefaultRestartButtonText;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            timer.ResultReached += onApplyConfirmedAsync;
        }

        private async Task onSaveClickAsync()
        {
            try
            {
                var mods = await JsRuntime.InvokeAsync<ModConfig[]>(JsMethods.ReadMods);
                for (int i = 0; i < mods.Count(); i++)
                    mods[i].Order = i;

                ModStorage.Set(mods);
                await JsRuntime.InvokeVoidAsync(JsMethods.ShowSuccess, "mods-save-btn");
            }
            catch (Exception)
            {
                await JsRuntime.InvokeVoidAsync(JsMethods.ShowFailure, "mods-save-btn");
            }

        }

        private void onApplyClick()
        {
            btnText = string.Format(Messages.ConfirmRestartButtonTemplate, timer.ClicksLeft);
            timer.Click();
        }

        private async Task onApplyConfirmedAsync(object? sender, bool isConfirmed)
        {
            timer.Reset();
            btnText = Messages.DefaultRestartButtonText;

            await InvokeAsync(() => { StateHasChanged(); });

            if (isConfirmed)
                await tryApplyMods();
        }

        private async Task tryApplyMods()
        {
            try
            {
                applyMods();
                await JsRuntime.InvokeVoidAsync(JsMethods.ShowSuccess, "mods-apply-btn");
            }
            catch (Exception)
            {
                await JsRuntime.InvokeVoidAsync(JsMethods.ShowFailure, "mods-apply-btn");
            }
        }

        private void applyMods()
        {
            var mods = ModStorage.Get();
            var collection = new ModCollection(mods);
            ModConfig.SetMods(collection);
        }
    }
}
