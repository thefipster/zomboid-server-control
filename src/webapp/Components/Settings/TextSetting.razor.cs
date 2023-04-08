using Microsoft.AspNetCore.Components;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Settings
{
    public partial class TextSetting
    {
        public const string SettingsType = "TextSetting";

        private string internalValue;

        [Parameter]
        public SettingsEntry Setting { get; set; }

        [Parameter]
        public EventCallback<SettingsUpdateEventArgs> OnValueChanged { get; set; }

        public TextSetting()
        {
            Setting = new();
        }

        protected override void OnParametersSet()
        {
            if (internalValue != default)
                return;

            internalValue = Setting.Default;
        }

        private async Task valueChanged(ChangeEventArgs e)
        {
            var args = new SettingsUpdateEventArgs(e.Value.ToString(), Setting.Key);
            await OnValueChanged.InvokeAsync(args);
        }
    }
}
