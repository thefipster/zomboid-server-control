using Microsoft.AspNetCore.Components;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Mods
{
    public partial class ModForm
    {
        private readonly ModFormData formData = new();

        [Parameter]
        public EventCallback<ModConfig> OnModAdded { get; set; }

        private async Task formSubmit()
        {
            var mod = new ModConfig(formData);
            formData.Reset();
            await OnModAdded.InvokeAsync(mod);
        }
    }
}
