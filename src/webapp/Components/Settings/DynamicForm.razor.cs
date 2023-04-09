using Microsoft.AspNetCore.Components;
using TheFipster.Zomboid.ServerControl.Config;
using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Components.Settings
{
    public partial class DynamicForm
    {
        private IniSettings settings;

        [Parameter]
        public EventCallback<SettingsUpdateEventArgs> OnValueChanged { get; set; }

        public DynamicForm() => settings = new();

        protected override void OnInitialized()
        {
            var iniValues = Service.GetSettings();
            foreach (var value in iniValues)
                foreach (var setting in Settings.Entries)
                    if (setting.Name == value.Key)
                        setting.Value = value.Value;

            settings = Settings;
        }

        private async Task valueChanged(SettingsUpdateEventArgs e)
        {
            await OnValueChanged.InvokeAsync(e);
        }
    }
}
