using TheFipster.Zomboid.ServerControl.Models;

namespace TheFipster.Zomboid.ServerControl.Pages
{
    public partial class ServerFile
    {
        public IniSettingsModel Model { get; set; }

        public ServerFile()
        {
            Model = new();
        }

        private async Task valueChanged(SettingsUpdateEventArgs e)
        {
            var type = typeof(IniSettingsModel);
            var prop = type.GetProperty(e.Property);
            prop.SetValue(Model, e.Value);
            await InvokeAsync(StateHasChanged);
        }
    }
}
