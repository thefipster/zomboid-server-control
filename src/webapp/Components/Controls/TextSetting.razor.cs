using Microsoft.AspNetCore.Components;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Controls
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
            base.OnParametersSet();

            if (internalValue != default)
                return;

            internalValue = Setting.Active;
        }

        private async Task valueChanged(ChangeEventArgs e)
        {
            var args = new SettingsUpdateEventArgs(e.Value.ToString(), Setting.Name);
            await OnValueChanged.InvokeAsync(args);
        }
    }
}
