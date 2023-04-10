using Microsoft.AspNetCore.Components;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Controls
{
    public partial class SettingsChooser
    {
        [Parameter]
        public SettingsEntry Setting { get; set; }

        [Parameter]
        public EventCallback<SettingsUpdateEventArgs> OnValueChanged { get; set; }

        public SettingsChooser()
        {
            Setting = new();
        }

        private async Task valueChanged(SettingsUpdateEventArgs e)
        {
            await OnValueChanged.InvokeAsync(e);
        }
    }
}
