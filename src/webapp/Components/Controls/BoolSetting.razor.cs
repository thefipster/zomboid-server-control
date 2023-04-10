using Microsoft.AspNetCore.Components;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Controls
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
            base.OnParametersSet();

            if (valueSet || Setting == null)
                return;

            valueSet = true;

            if (bool.TryParse(Setting.Active.ToLower(), out var toggle))
            {
                internalValue = toggle;
            }
        }

        private async Task valueChanged(ChangeEventArgs e)
        {
            var args = new SettingsUpdateEventArgs(e.Value.ToString().ToLower(), Setting.Name);
            await OnValueChanged.InvokeAsync(args);
        }
    }
}
