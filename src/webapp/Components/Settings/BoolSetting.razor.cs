using Microsoft.AspNetCore.Components;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Settings
{
    public partial class BoolSetting
    {
        public const string SettingsType = "BoolSetting";

        private bool internalValue;
        private bool valueSet;

        [Parameter]
        public SettingsEntry? Setting { get; set; }

        [Parameter]
        public EventCallback<SettingsUpdateEventArgs> OnValueChanged { get; set; }

        public BoolSetting()
        {
            Setting = new();
        }

        protected override void OnParametersSet()
        {
            if (valueSet)
                return;

            valueSet= true;

            if (bool.TryParse(Setting.Default.ToLower(), out var toggle))
            {
                internalValue = toggle;
            }
        }

        private async Task valueChanged(ChangeEventArgs e)
        {
            var args = new SettingsUpdateEventArgs(e.Value.ToString().ToLower(), Setting.Key);
            await OnValueChanged.InvokeAsync(args);
        }
    }
}
