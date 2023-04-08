using Microsoft.AspNetCore.Components;
using TheFipster.Zomboid.ServerControl.Config;

namespace TheFipster.Zomboid.ServerControl.Components.Settings
{
    public partial class CoordinateSetting
    {
        public const string SettingsType = "CoordinateSetting";

        [Parameter]
        public SettingsEntry Setting { get; set; }

        [Parameter]
        public EventCallback<string> OnValueChanged { get; set; }
    }
}
